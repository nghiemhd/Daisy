using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class Blog : BaseEntity
    {
        public Blog()
        {
            Photos = new HashSet<Photo>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsPublished { get; set; }

        public string Highlight { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
