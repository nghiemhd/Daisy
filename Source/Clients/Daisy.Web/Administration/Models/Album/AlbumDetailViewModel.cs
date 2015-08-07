using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class AlbumDetailViewModel
    {
        public Album Album { get; set; }
        public IList<Photo> Photos { get; set; }
    }
}