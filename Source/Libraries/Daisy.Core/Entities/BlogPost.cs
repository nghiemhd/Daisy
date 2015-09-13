using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class BlogPost : BaseEntity
    {
        public int LanguageId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsPublished { get; set; }

        public string Highlight { get; set; }

        public string ImageUrl { get; set; }

        public string Tags { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }

        public string MetaTitle { get; set; }

        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
    }
}
