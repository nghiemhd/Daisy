using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class Photo : BaseEntity
    {
        public Photo()
        {
            Albums = new HashSet<Album>();
            Sliders = new HashSet<Slider>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string SmallUrl { get; set; }

        public string MediumUrl { get; set; }

        public string LargeUrl { get; set; }

        public string Large1600Url { get; set; }

        public string Large2048Url { get; set; }

        public string OriginalUrl { get; set; }

        public string FlickrPhotoId { get; set; }

        public byte[] Content { get; set; }

        public bool IsPublished { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Slider> Sliders { get; set; }
    }
}
