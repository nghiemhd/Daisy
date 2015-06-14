using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class AlbumService : IAlbumService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Album> albumRepository;
        private IFlickrService flickrService;

        public AlbumService(IUnitOfWork unitOfWork, IFlickrService flickrService)
        {
            this.unitOfWork = unitOfWork;
            albumRepository = this.unitOfWork.GetRepository<Album>();
            this.flickrService = flickrService;
        }

        public PhotosetCollection GetAllAlbumsFromFlickr(string userId)
        {
            return flickrService.GetAllAlbums(userId);
        }

        public PhotosetPhotoCollection GetPhotosByAlbumFromFlickr(string albumId)
        {
            return flickrService.GetPhotosByAlbum(albumId);            
        }
    }
}
