using Daisy.Common;
using DaisyEntities = Daisy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daisy.Service.DataContracts;

namespace Daisy.Service.ServiceContracts
{
    public interface IPhotoService
    {
        IEnumerable<DaisyEntities.Photo> GetAllPhotos();
        IEnumerable<DaisyEntities.Photo> GetDisplayedPhotos();
        DaisyEntities.Photo GetPhotoById(int id);
        void DeletePhotoById(int id);
        void DeletePhotos(IEnumerable<DaisyEntities.Photo> photos);
        FlickrNet.PhotoInfo GetFlickrPhotoInfo(string id);
        PagedList<DaisyEntities.Photo> SearchPhotos(SearchPhotoOptions options);
        IEnumerable<DaisyEntities.Photo> GetPhotosBy(int[] photoIds);
    }
}
