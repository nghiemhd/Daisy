using Daisy.Common;
using Daisy.Service.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var uploadPath = ConfigurationManager.AppSettings[Constants.UploadPath];
            uploadPath = Server.MapPath(uploadPath);

            uploadService.Upload(file, uploadPath);

            ViewBag.Message = "Upload successfully";

            return View("Index");
        }
    }
}