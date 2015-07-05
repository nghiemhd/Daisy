using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Daisy.Admin.Models
{
    public class Album
    {
        public string FlickrAlbumId { get; set; }

        public string Name { get; set; }

        public string AlbumThumbnailUrl { get; set; }
    }
}