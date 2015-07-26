using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class Photo
    {
        public string FlickrPhotoId { get; set; }

        public string Name { get; set; }

        public string SmallUrl { get; set; }

        public string MediumUrl { get; set; }

        public string LargeUrl { get; set; }

        public string OriginalUrl { get; set; }
    }
}