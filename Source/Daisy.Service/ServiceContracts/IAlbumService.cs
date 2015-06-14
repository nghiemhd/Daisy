using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;

namespace Daisy.Service.ServiceContracts
{
    public interface IAlbumService
    {
        PhotosetCollection GetAllAlbumsFromFlickr(string userId);
        PhotosetPhotoCollection GetPhotosByAlbumFromFlickr(string albumId);
    }
}
