using Daisy.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Service.DataContracts
{
    public class DataResponse
    {
        public ResponseStatus Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
