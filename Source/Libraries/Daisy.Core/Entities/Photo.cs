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
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsDisplayed { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
