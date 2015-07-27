using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Core.Entities
{
    public class AlbumPhoto
    {
        [Key, Column(Order = 0)]
        public int AlbumId { get; set; }
        [Key, Column(Order = 1)]
        public int PhotoId { get; set; }
    }
}
