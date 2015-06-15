using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class SearchAlbumModel
    {
        public string UserId { get; set; }
        public string AlbumName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}