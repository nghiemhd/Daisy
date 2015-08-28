using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class SearchBlogModel
    {
        public string Title { get; set; }

        [DisplayName("From")]
        public DateTime? FromDate { get; set; }

        [DisplayName("To")]
        public DateTime? ToDate { get; set; }

        [DisplayName("Status")]
        public bool? IsPublished { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}