using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daisy.Admin.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name="Published")]
        public bool IsPublished { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        [MaxLength(500)]
        public string Highlight { get; set; }

        [Display(Name="Image")]
        public string ImageUrl { get; set; }

        [DisplayName("Friendly url")]
        [MaxLength(100)]
        public string Slug { get; set; }

        public string Tags { get; set; }

        [DisplayName("Meta keywords")]
        [MaxLength(400)]
        public string MetaKeywords { get; set; }

        [DisplayName("Meta title")]
        [MaxLength(400)]
        public string MetaTitle { get; set; }

        [DisplayName("Meta description")]
        public string MetaDescription { get; set; }

        public List<Language> Languages { get; set; }
    }
}