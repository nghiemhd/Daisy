using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class CategoryPhoto
    {
        public int CategoryId { get; set; }

        public int PhotoId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual Category Category { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
