using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class BlogPhoto
    {
        [Key, Column(Order = 0), ForeignKey("Blog")]        
        public int BlogId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Photo")]
        public int PhotoId { get; set; }

        public int Order { get; set; }

        public string Position { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual Photo Photo { get; set; }
    }
}
