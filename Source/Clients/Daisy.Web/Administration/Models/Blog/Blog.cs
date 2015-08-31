using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [Display(Name="Published")]
        public bool IsPublished { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}