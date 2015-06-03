using Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface IPhotoService
    {
        IEnumerable<Photo> GetAllPhotos();
        IEnumerable<Photo> GetDisplayedPhotos();
        Photo GetPhotoById(int id);
        void DeletePhotoById(int id);
        void DeletePhotos(IEnumerable<Photo> photos);
    }
}
