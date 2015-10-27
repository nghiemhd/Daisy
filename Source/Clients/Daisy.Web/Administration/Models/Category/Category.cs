using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Daisy.Admin.Models
{
    public class Category
    {
        public Category()
        {
            Languages = new List<Language>();
            Photos = new List<Photo>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int TemplateId { get; set; }

        [DisplayName("Friendly url")]
        [MaxLength(100)]
        public string Slug { get; set; }

        [DisplayName("Meta keywords")]
        [MaxLength(400)]
        public string MetaKeywords { get; set; }

        [DisplayName("Meta title")]
        [MaxLength(400)]
        public string MetaTitle { get; set; }

        [DisplayName("Meta description")]
        public string MetaDescription { get; set; }

        public int ParentCategoryId { get; set; }

        public int PhotoId { get; set; }

        public int PageSize { get; set; }

        [Display(Name = "Published")]
        public bool IsPublished { get; set; }

        public int DisplayOrder { get; set; }

        public int LanguageId { get; set; }

        public string Language { get; set; }

        public IList<Language> Languages { get; set; }

        public IList<Photo> Photos { get; set; }
    }
}