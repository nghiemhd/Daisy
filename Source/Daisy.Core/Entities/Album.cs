using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class Album : BaseEntity
    {
        public Album()
        {
            Photos = new HashSet<Photo>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool IsDisplayed { get; set; }

        [MaxLength(100)]
        public string FlickrAlbumId { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
