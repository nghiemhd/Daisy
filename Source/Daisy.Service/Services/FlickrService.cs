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
    }
}
