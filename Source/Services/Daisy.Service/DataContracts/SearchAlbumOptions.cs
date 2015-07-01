using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class SearchAlbumOptions
    {
        public string UserId { get; set; }
        public string AlbumName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
