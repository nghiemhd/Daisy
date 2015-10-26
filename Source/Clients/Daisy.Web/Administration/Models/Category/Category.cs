using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TemplateId { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaTitle { get; set; }

        public int ParentCategoryId { get; set; }

        public int PhotoId { get; set; }

        public int PageSize { get; set; }

        public bool IsPublished { get; set; }

        public int DisplayOrder { get; set; }

        public IList<Photo> Photos { get; set; }
    }
}