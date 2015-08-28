using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daisy.Admin.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}