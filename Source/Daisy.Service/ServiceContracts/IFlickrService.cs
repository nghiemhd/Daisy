using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.ServiceContracts
{
    public interface IFlickrService
    {
        PhotosetCollection GetAllAlbums(string userId);
        PhotosetPhotoCollection GetPhotosByAlbum(string photosetId);
    }
}
