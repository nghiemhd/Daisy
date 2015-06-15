using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using Daisy.Common;
using Daisy.Service.DataContracts;

namespace Daisy.Service.ServiceContracts
{
    public interface IAlbumService
    {
        PhotosetCollection GetAllAlbumsFromFlickr(string userId);

        PagedList<Photoset> GetAlbumsFromFlickr(SearchAlbumOptions options);

        PhotosetPhotoCollection GetPhotosByAlbumFromFlickr(string albumId);
    }
}
