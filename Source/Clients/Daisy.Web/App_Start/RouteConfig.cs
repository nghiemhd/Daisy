using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Daisy.Web.Framework.Localization;
using Daisy.Web.Framework.Seo;

namespace Daisy.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Blog",
                url: "blog",
                defaults: new { controller = "Blog", action = "Index" },
                namespaces: new[] { "Daisy.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Album",
                url: "album",
                defaults: new { controller = "Album", action = "Index" },
                namespaces: new[] { "Daisy.Web.Controllers" }
            );

            routes.MapGenericPathRoute("GenericUrl",
                                       "{generic_se_name}",
                                       new { controller = "Common", action = "GenericUrl" },
                                       new[] { "Daisy.Web.Controllers" });            

            routes.MapRoute(
                name: "BlogPost",
                url: "blog/{slug}",
                defaults: new { controller = "Blog", action = "Detail" },
                namespaces: new[] { "Daisy.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Daisy.Web.Controllers" }
            );            
        }
    }
}
