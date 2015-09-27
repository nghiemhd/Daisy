using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class SliderPhoto
    {
        public int SliderId { get; set; }

        public int PhotoId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Slider Slider { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
