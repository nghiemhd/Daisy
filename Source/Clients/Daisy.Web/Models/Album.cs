using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Web.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AlbumThumbnailUrl { get; set; }
    }
}