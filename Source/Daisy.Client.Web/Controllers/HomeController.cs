using Daisy.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daisy.Client.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new PhotosViewModel
            {
                Photos = new List<Photo> { 
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },   
                    new Photo { Url = "https://c1.staticflickr.com/9/8816/17845449760_8a06d80820_h.jpg" },
                }
            };
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