using Daisy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class PagedListBlogViewModel
    {
        public SearchBlogModel SearchOptions { get; set; }
        public PagedList<Blog> Blogs { get; set; }
    }
}