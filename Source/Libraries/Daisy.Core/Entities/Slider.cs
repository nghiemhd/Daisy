using Daisy.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class Slider : BaseEntity
    {
        public Slider()
        {
            Photos = new HashSet<Photo>();
        }

        public string Name { get; set; }

        public string Page { get; set; }

        public int Order { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
