using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Web.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}