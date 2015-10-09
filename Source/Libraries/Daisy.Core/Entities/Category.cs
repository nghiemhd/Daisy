using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            CategoryPhotos = new HashSet<CategoryPhoto>();
        }

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

        public virtual ICollection<CategoryPhoto> CategoryPhotos { get; set; }
    }
}
