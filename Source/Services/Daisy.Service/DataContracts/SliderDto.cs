using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class SliderDto
    {
        public string Name { get; set; }

        public string Page { get; set; }

        public int Order { get; set; }

        public IList<int> PhotoIds { get; set; }
    }
}
