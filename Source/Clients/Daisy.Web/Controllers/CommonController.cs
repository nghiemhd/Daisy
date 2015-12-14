using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Daisy.Web.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult PageNotFound()
        {
            //seems that no entity was found
            return PartialView("PageNotFound");
        }        
    }
}