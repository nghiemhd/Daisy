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

        public PhotoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            photoRepository = this.unitOfWork.GetRepository<Photo>();
        }

        public IEnumerable<Photo> GetAllPhotos()
        {
            return photoRepository.GetAll().ToList();
        }

        public IEnumerable<Photo> GetDisplayedPhotos()
        {
            return photoRepository.GetAll().Where(x => x.IsDisplayed == true).ToList();
        }

        public Photo GetPhotoById(int id)
        {
            var photo = photoRepository
                .GetAll()
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
    }
}
