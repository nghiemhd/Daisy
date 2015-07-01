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
        private readonly IPhotoService photoService;

        public HomeController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public ActionResult Index()
        {
            //var model = new PhotosViewModel
            //{
            //    Photos = new List<Photo> { 
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
            //        new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
            //    }
            //};

            var options = new PhotoSearchOptions();
            options.PerPage = 10;
            options.Page = 1;
            options.MediaType = MediaType.Photos;
            options.Extras = PhotoSearchExtras.All;
            options.UserId = "96231191@N07";

            Flickr flickr = new Flickr("95b00fe5ec213d2e9dc52ebf72cea4e0", "a9f96d07c3481f94");
            var test = flickr.PhotosSearch(options);

            var albums = flickr.PhotosetsGetList(options.UserId);
            var photos = photoService.GetDisplayedPhotos();
            var model = new Models.PhotosViewModel();
            model.Photos = Mapper.Map<List<Models.Photo>>(photos);

            return View(model);
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
    }
}