using Daisy.Logging;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Daisy.Service
{
    public class UploadService :  IUploadService
    {
        private ILogger logger;

        public UploadService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Upload(HttpPostedFileBase file, string uploadPath)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(uploadPath, fileName);
                    
                    file.SaveAs(path);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}
