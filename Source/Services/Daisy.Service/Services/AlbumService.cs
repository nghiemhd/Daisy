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

namespace Daisy.Service
{
    public class AlbumService : IAlbumService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<DaisyEntities.Album> albumRepository;
        private IFlickrService flickrService;

        public AlbumService(IUnitOfWork unitOfWork, IFlickrService flickrService)
        {
            this.unitOfWork = unitOfWork;
            albumRepository = this.unitOfWork.GetRepository<DaisyEntities.Album>();
            this.flickrService = flickrService;
        }

        public PhotosetCollection GetAllFlickrAlbums(string userId)
        {
            return flickrService.GetAllAlbums(userId);
        }

        public PagedList<Photoset> GetFlickrAlbums(SearchAlbumOptions options)
        {
            return flickrService.GetAlbums(options);
        }

        public PhotosetPhotoCollection GetPhotosByFlickrAlbum(string albumId)
        {
            return flickrService.GetPhotosByAlbum(albumId);            
        }

        public void ImportAlbums(IEnumerable<DaisyEntities.Album> entities)
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

        public Photoset GetFlickrAlbumById(string id)
        {
            return flickrService.GetAlbumById(id);
        }
    }
}
