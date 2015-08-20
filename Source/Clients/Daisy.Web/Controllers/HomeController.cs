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

        private List<DaisyModels.Photo> GetPhotos()
        {
            return new List<DaisyModels.Photo>
            { 
                new DaisyModels.Photo{ Large1600Url = "https://farm9.staticflickr.com/8705/17016677211_4715fb69c8_h.jpg", Large2048Url = "https://farm9.staticflickr.com/8705/17016677211_420329ec88_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm8.staticflickr.com/7624/16397422673_0d3e5665e3_h.jpg", Large2048Url = "https://farm8.staticflickr.com/7624/16397422673_dcbd2e3f9c_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm9.staticflickr.com/8689/16810151517_833e02b92c_h.jpg", Large2048Url = "https://farm9.staticflickr.com/8689/16810151517_bfeb5c20e3_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm9.staticflickr.com/8703/16991562816_79e8450d5d_h.jpg", Large2048Url = "https://farm9.staticflickr.com/8703/16991562816_1565a3e4b8_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm9.staticflickr.com/8734/17017549685_492c084813_h.jpg", Large2048Url = "https://farm9.staticflickr.com/8734/17017549685_cf2d77a332_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm8.staticflickr.com/7592/16397417673_c03b2033d4_h.jpg", Large2048Url = "https://farm8.staticflickr.com/7592/16397417673_3b9fc8b575_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm8.staticflickr.com/7626/16395129314_18caa7b1d0_h.jpg", Large2048Url = "https://farm8.staticflickr.com/7626/16395129314_d60d9bc222_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm8.staticflickr.com/7650/16830021960_62da296e47_h.jpg", Large2048Url = "https://farm8.staticflickr.com/7650/16830021960_db27a55684_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm8.staticflickr.com/7650/16830021960_62da296e47_h.jpg", Large2048Url = "https://farm8.staticflickr.com/7592/16397417673_3b9fc8b575_k.jpg" },
                new DaisyModels.Photo{ Large1600Url = "https://farm8.staticflickr.com/7650/16830021960_62da296e47_h.jpg", Large2048Url = "https://farm8.staticflickr.com/7592/16397417673_3b9fc8b575_k.jpg" },
            };
        }
    }
}