using Daisy.Common;
using Daisy.Service.DataContracts;
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
        PagedList<Photoset> GetAlbums(SearchAlbumOptions options);
        PhotosetPhotoCollection GetPhotosByAlbum(string photosetId);
    }
}
