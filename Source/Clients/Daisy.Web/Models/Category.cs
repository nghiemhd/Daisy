using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PageSize { get; set; }

        public bool IsPublish { get; set; }

        public int LanguageId { get; set; }

        public string Language { get; set; }

        public string Slug { get; set; }

        public IList<Photo> Photos { get; set; }
    }
}