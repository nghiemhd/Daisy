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
        public ActionResult GenericUrl()
        {
            //seems that no entity was found
            return InvokeHttp404();
        }

        protected virtual ActionResult InvokeHttp404()
        {
            // Call target Controller and pass the routeData.
            //IController errorController = EngineContext.Current.Resolve<Nop.Web.Controllers.CommonController>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            //errorController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }
    }
}