using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Daisy.Common;
using Daisy.Common.Extensions;
using Daisy.Core.Infrastructure;
using Daisy.Logging;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using DaisyEntities = Daisy.Core.Entities;
using FlickrNet;
using System.Threading;

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

        public PagedList<Photoset> SearchFlickrAlbums(SearchAlbumOptions options)
        {
            try
            {
                return flickrService.SearchAlbums(options);
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
                    var albumsInDb = albumRepository.Query()
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
                var albums = albumRepository.Query()
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

        public PagedList<DaisyEntities.Album> SearchAlbums(SearchAlbumOptions options)
        {
            try
            {
                var query = albumRepository.Query();
                if (!options.AlbumName.IsNullOrEmpty())
                {
                    query = query.Where(x => x.Name.Contains(options.AlbumName));
                }

                if (options.IsPublished != null)
                {
                    query = query.Where(x => x.IsPublished == options.IsPublished);
                }

                if (options.PageSize <= 0 || options.PageSize > Constants.MaxPageSize)
                {
                    options.PageSize = Constants.DefaultPageSize;
                }

                if (options.PageIndex < 0)
                {
                    options.PageIndex = 0;
                }

                int totalCount = query.Count();
                query = query
                        .OrderBy(x => x.Id)
                        .Skip(options.PageSize * options.PageIndex)
                        .Take(options.PageSize);

                var result = new PagedList<DaisyEntities.Album>(
                    query.ToList(),
                    options.PageIndex,
                    options.PageSize,
                    totalCount
                );

                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public DaisyEntities.Album GetAlbumById(int id)
        {
            var album = albumRepository.Query().SingleOrDefault(x => x.Id == id);
            return album;
        }

        public void PublishAlbums(IList<int> albumIds, bool isPublished)
        {
            try
            {
                var updatedAlbums = albumRepository.Query()
                    .Where(x => albumIds.Contains(x.Id) && x.IsPublished != isPublished).ToList();
                updatedAlbums.ForEach(album => { 
                    album.IsPublished = isPublished;
                    album.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                    album.UpdatedDate = DateTime.Now;
                    album.Photos.ToList().ForEach(photo => { 
                        photo.IsPublished = isPublished;
                        photo.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                        photo.UpdatedDate = DateTime.Now;
                    });
                });

                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public void PublishPhotos(int albumId, IList<int> photoIds, bool isPublished)
        {
            try
            {
                if(!isPublished)
                {
                    UnpublishPhotos(photoIds);
                }
                else
                {
                    var album = albumRepository.Query()
                        .Where(x => x.Id == albumId).FirstOrDefault();
                    if (album != null)
                    {
                        if (!album.IsPublished)
                        {
                            album.IsPublished = true;
                            album.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                            album.UpdatedDate = DateTime.Now;
                        }
                        album.Photos.Where(x => photoIds.Contains(x.Id) && !x.IsPublished)
                        .ToList().ForEach(photo =>
                        {
                            photo.IsPublished = true;
                            photo.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                            photo.UpdatedDate = DateTime.Now;
                        });
                    }
                }
                this.unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;                
            }
        }

        private void UnpublishPhotos(IList<int> photoIds)
        {
            var photos = photoRepository.Query()
                .Where(x => photoIds.Contains(x.Id) && x.IsPublished)
                .ToList();
            photos.ForEach(photo => {
                photo.IsPublished = false;
                photo.UpdatedBy = Thread.CurrentPrincipal.Identity.Name;
                photo.UpdatedDate = DateTime.Now;
            });
        }
    }
}
