using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class SearchPhotoModel
    {
        public string UserId { get; set; }

        [DisplayName("Album name")]
        public string AlbumName { get; set; }

        [DisplayName("Status")]
        public bool? IsPublished { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}