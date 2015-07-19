using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlickrNet;
using Daisy.Common;
using Daisy.Service.DataContracts;
using DaisyEntities = Daisy.Core.Entities;

namespace Daisy.Service.ServiceContracts
{
    public interface IAlbumService
    {
        PhotosetCollection GetAllFlickrAlbums(string userId);

        PagedList<Photoset> GetFlickrAlbums(SearchAlbumOptions options);

        PhotosetPhotoCollection GetPhotosByFlickrAlbum(string albumId);

        void ImportAlbums(IEnumerable<DaisyEntities.Album> entities);
    }
}
