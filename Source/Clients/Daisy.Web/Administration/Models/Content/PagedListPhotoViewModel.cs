using Daisy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class PagedListPhotoViewModel
    {
        public SearchPhotoModel SearchOptions { get; set; }
        public PagedList<Photo> Photos { get; set; }
    }
}