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
            SliderPhotos = new HashSet<SliderPhoto>();
        }

        public string Name { get; set; }

        public string Page { get; set; }

        public int Order { get; set; }

        public virtual ICollection<SliderPhoto> SliderPhotos { get; set; }
    }
}
