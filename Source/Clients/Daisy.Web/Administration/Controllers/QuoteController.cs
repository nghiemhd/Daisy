using Daisy.Common;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class QuoteController : Controller
    {
        private readonly IUploadService uploadService;

        public QuoteController(IUploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        public ActionResult Index()
        {
            ViewBag.maxAllowedContentLength = GetMaxFileUploadSize();
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            int maxFileSize = GetMaxFileUploadSize();
            if (file.ContentLength > maxFileSize)
            {
                TempData["error"] = string.Format("Cannot upload file more than {0} bytes", maxFileSize);
                return RedirectToAction("Index");
            }
            
            var uploadPath = ConfigurationManager.AppSettings[Constants.QuotePath];
            if (uploadPath != Path.GetFullPath(uploadPath))
            {
                var rootPath = Server.MapPath("~");
                uploadPath = Path.Combine(rootPath, uploadPath);
            }

            uploadService.Upload(file, uploadPath);

            TempData["message"] = "Upload successfully.";

            return RedirectToAction("Index");
        }

        private int GetMaxFileUploadSize()
        {
            var config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
            var section = config.GetSection("system.webServer");
            var xml = section.SectionInformation.GetRawXml();
            var doc = XDocument.Parse(xml);
            var element = doc.Root.Element("security").Element("requestFiltering").Element("requestLimits");
            string value = element.Attribute("maxAllowedContentLength").Value;

            int result;
            int.TryParse(value, out result);

            return result;
        }
    }
}