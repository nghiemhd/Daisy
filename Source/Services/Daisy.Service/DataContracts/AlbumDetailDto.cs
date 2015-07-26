using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class AlbumDetailDto
    {
        public AlbumDto Album { get; set; }

        public IList<PhotoDto> Photos { get; set; }
    }
}
