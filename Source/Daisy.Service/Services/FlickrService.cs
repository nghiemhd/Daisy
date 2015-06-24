using Daisy.Common;
using Daisy.Common.Extensions;
using Daisy.Service.DataContracts;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service
{
    public class FlickrService : IFlickrService
    {
        private readonly Flickr flickr;

        public FlickrService()
        {
            var apiKey = ConfigurationManager.AppSettings[Constants.FlickrApiKey];
            var sharedSecret = ConfigurationManager.AppSettings[Constants.FlickrSharedSecret];
            flickr = new Flickr(apiKey, sharedSecret);
        }

        public FlickrService(string apiKey, string sharedSecret)
        {
            flickr = new Flickr(apiKey, sharedSecret);
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
                if (options == null)
                {
                    throw new ArgumentNullException("options");
                }

                if (options.UserId.IsNullOrEmpty())
                {
                    options.UserId = ConfigurationManager.AppSettings[Constants.FlickrUserId];
                }

                IEnumerable<Photoset> albums = flickr.PhotosetsGetList(options.UserId);
                
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

                int totalCount = albums.Count();
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
