using Daisy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Web.Models
{
    public class PagedListBlogViewModel
    {
        public PagedList<Blog> Blogs { get; set; }
    }
}