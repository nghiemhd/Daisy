using Daisy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class PagedListAlbumViewModel
    {
        public SearchAlbumModel SearchOptions { get; set; }
        public PagedList<Album> Albums { get; set; }
    }
}