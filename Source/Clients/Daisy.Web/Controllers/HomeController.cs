using AutoMapper;
using Models = Daisy.Web.Models;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;

namespace Daisy.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {                        
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileResult Quote()
        {
            var quotePath = ConfigurationManager.AppSettings["QuotePath"];
            var filePath = Path.Combine(quotePath, "quote.pdf");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "quote.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}