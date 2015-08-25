using AutoMapper;
using DaisyModels = Daisy.Web.Models;
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
        private readonly IContentService contentService;

        public HomeController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public ActionResult Index()
        {
            var photos = contentService.GetFirstSlider().Photos.ToList();
            var model = Mapper.Map<List<DaisyModels.Photo>>(photos);
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public FileResult Quote()
        {
            var quotePath = ConfigurationManager.AppSettings["QuotePath"];
            if (quotePath != Path.GetFullPath(quotePath))
            {
                var rootPath = Server.MapPath("~");
                quotePath = Path.Combine(rootPath, quotePath);
            }

            var directory = new DirectoryInfo(quotePath);
            var latestQuote = directory
                .GetFiles()
                .OrderByDescending(x => x.LastWriteTime)
                .FirstOrDefault();

            byte[] fileBytes = System.IO.File.ReadAllBytes(latestQuote.FullName);
            string fileName = latestQuote.Name;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}