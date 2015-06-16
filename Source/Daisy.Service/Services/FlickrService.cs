using Daisy.Common;
using Daisy.Common.Extensions;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class FlickrService : IFlickrService
    {
        private readonly Flickr flickr;

        public FlickrService(string apiKey, string sharedSecret)
        {
            flickr = new Flickr(apiKey, sharedSecret);
        }

        public FlickrService(string apiKey, string sharedSecret, string token)
        {
            flickr = new Flickr(apiKey, sharedSecret, token);
        }

        public PhotosetCollection GetAllAlbums(string userId)
        {
            return flickr.PhotosetsGetList(userId);
        }

        public PhotosetPhotoCollection GetPhotosByAlbum(string photosetId)
        {            
            return flickr.PhotosetsGetPhotos(photosetId);
        }

        public PagedList<Photoset> GetAlbums(SearchAlbumOptions options)
        {
            try
            {
                IEnumerable<Photoset> albums = flickr.PhotosetsGetList(options.UserId);
                int totalCount = albums.Count();

                if (options.PageSize <= 0 || options.PageSize > Constants.MaxPageSize)
                {
                    options.PageSize = Constants.DefaultPageSize;
                }
                if (options.PageIndex < 0)
                {
                    options.PageIndex = 0;
                }

                if (!options.AlbumName.IsNullOrEmpty())
                {
                    albums = albums
                        .Where(x => x.Title.IndexOf(options.AlbumName, StringComparison.OrdinalIgnoreCase) >= 0);                        
                }

                albums = albums
                        .Skip(options.PageSize * options.PageIndex)
                        .Take(options.PageSize);

                PagedList<Photoset> result = new PagedList<Photoset>(
                    albums,
                    options.PageIndex,
                    options.PageSize,
                    totalCount
                );

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
