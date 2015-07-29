using System.Collections.Generic;

using Daisy.Common;
using Daisy.Service.DataContracts;
using DaisyEntities = Daisy.Core.Entities;
using FlickrNet;

namespace Daisy.Service.ServiceContracts
{
    public interface IAlbumService
    {
        PhotosetCollection GetAllFlickrAlbums(string userId);

        PagedList<Photoset> SearchFlickrAlbums(SearchAlbumOptions options);

        PhotosetPhotoCollection GetPhotosByFlickrAlbum(string albumId);

        void ImportAlbums(IEnumerable<DaisyEntities.Album> entities);

        Photoset GetFlickrAlbumById(string id);

        IEnumerable<DaisyEntities.Album> FindAlbum(string flickrAlbumId);

        void ImportAlbumDetail(AlbumDetailDto album);

        PagedList<DaisyEntities.Album> SearchAlbums(SearchAlbumOptions options);

        DaisyEntities.Album GetAlbumById(int id);

        void PublishAlbums(IList<int> albumIds, bool isPublished);        
    }
}
