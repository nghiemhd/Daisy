using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Daisy.Service.ServiceContracts
{
    public interface IUploadService
    {
        void Upload(HttpPostedFileBase file, string uploadPath);
    }
}
