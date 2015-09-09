using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class SearchBlogModel
    {
        [DisplayName("Language")]
        public int? LanguageId { get; set; }

        public string Title { get; set; }

        [DisplayName("From")]
        public DateTime? FromCreatedDate { get; set; }

        [DisplayName("To")]
        public DateTime? ToCreatedDate { get; set; }

        [DisplayName("Status")]
        public bool? IsPublished { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}