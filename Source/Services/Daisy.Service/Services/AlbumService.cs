using Daisy.Common;
using DaisyEntities = Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using EntityFramework.BulkInsert.Extensions;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using System.Transactions;
using System.Data.Entity.Core;
using Daisy.Logging;

namespace Daisy.Service
{
    public class AlbumService : IAlbumService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Album> albumRepository;
        private IRepository<DaisyEntities.Photo> photoRepository;
        private ILogger logger;        
        private IFlickrService flickrService;

        public AlbumService(IUnitOfWork unitOfWork, ILogger logger, IFlickrService flickrService)
        {
            this.unitOfWork = unitOfWork;
            this.albumRepository = this.unitOfWork.GetRepository<DaisyEntities.Album>();
            this.photoRepository = this.unitOfWork.GetRepository<DaisyEntities.Photo>();
            this.logger = logger;
            this.flickrService = flickrService;
        }

        public PhotosetCollection GetAllFlickrAlbums(string userId)
        {
            try
            {
                return flickrService.GetAllAlbums(userId);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public PagedList<Photoset> GetFlickrAlbums(SearchAlbumOptions options)
        {
            try
            {
                return flickrService.GetAlbums(options);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public PhotosetPhotoCollection GetPhotosByFlickrAlbum(string albumId)
        {
            try
            {
                return flickrService.GetPhotosByAlbum(albumId);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void ImportAlbums(IEnumerable<DaisyEntities.Album> entities)
        {
            try
            {
                if (entities != null && entities.Count() > 0)
                {
                    var newAlbumIds = entities.Select(x => x.FlickrAlbumId).ToArray();
                    var albumsInDb = albumRepository.GetAll()
                        .Where(x => newAlbumIds.Contains(x.FlickrAlbumId))
                        .Select(x => x.FlickrAlbumId).ToArray();
                    var albumsNotInDb = entities.Where(x => !albumsInDb.Contains(x.FlickrAlbumId));

                    foreach (var album in albumsNotInDb)
                    {
                        var photos = flickrService.GetPhotosByAlbum(album.FlickrAlbumId);
                        album.Photos = Mapper.Map<List<DaisyEntities.Photo>>(photos);

                        albumRepository.Insert(album);
                    }

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public Photoset GetFlickrAlbumById(string id)
        {
            return flickrService.GetAlbumById(id);
        }

        public IEnumerable<DaisyEntities.Album> FindAlbum(string flickrAlbumId)
        {
            try
            {
                var albums = albumRepository.GetAll()
                    .Where(x => x.FlickrAlbumId == flickrAlbumId)
                    .ToArray();
                return albums;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void ImportAlbumDetail(AlbumDetailDto albumDetail)
        {
            try
            {
                var album = FindAlbum(albumDetail.Album.FlickrAlbumId).FirstOrDefault();
                var photos = Mapper.Map<List<DaisyEntities.Photo>>(albumDetail.Photos);
                if (album == null)
                {
                    album = Mapper.Map<DaisyEntities.Album>(albumDetail.Album);
                    album.Photos = photos;

                    albumRepository.Insert(album);
                }
                else
                {
                    var photosInDb = album.Photos.Select(x => x.FlickrPhotoId);
                    var photosNotInDb = photos.Where(x => !photosInDb.Contains(x.FlickrPhotoId)).ToList();
                    foreach (var photo in photosNotInDb)
                    {
                        album.Photos.Add(photo);
                    }
                }

                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
