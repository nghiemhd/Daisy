using AutoMapper;
using Models = Daisy.Web.Models;
using Daisy.Service.ServiceContracts;
using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"D:\Programming Projects\Daisy\Source\Clients\Daisy.Admin\Upload\Quotes\avatar.jpg");
            string fileName = "avatar.jpg";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}