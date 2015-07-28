using Daisy.Core.Entities;
using Daisy.Core.Infrastructure;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class PhotoService : IPhotoService
    {
        private IUnitOfWork unitOfWork;
        private IRepository<Photo> photoRepository;
        private IFlickrService flickrService;

        public PhotoService(IUnitOfWork unitOfWork, IFlickrService flickrService)
        {
            this.unitOfWork = unitOfWork;
            photoRepository = this.unitOfWork.GetRepository<Photo>();
            this.flickrService = flickrService;
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            return photoRepository.Query().ToList();
        }

        public IEnumerable<Photo> GetDisplayedPhotos()
        {
            return photoRepository.Query().Where(x => x.IsPublished == true).ToList();
        }

        public Photo GetPhotoById(int id)
        {
            var photo = photoRepository
                .Query()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return photo;
        }

        public void DeletePhotoById(int id)
        {
            var photo = GetPhotoById(id);
            photoRepository.Delete(photo);
            unitOfWork.Commit();
        }

        public void DeletePhotos(IEnumerable<Photo> photos)
        {
            throw new NotImplementedException();
        }

        public FlickrNet.PhotoInfo GetFlickrPhotoInfo(string id)
        {
            return flickrService.GetPhotoInfo(id);
        }
    }
}
