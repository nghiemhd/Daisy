using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Daisy.Web.Framework.ViewEngines.Razor
{
    public class CustomRazorViewEngine : CustomVirtualPathProviderViewEngine
    {
        public CustomRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };

            AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml",
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/Shared/{0}.cshtml",

                //Admin
                "~/Administration/Views/{1}/{0}.cshtml",
                "~/Administration/Views/Shared/{0}.cshtml",
            };

            MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
            "~/Views/{1}/{0}.cshtml", 
            "~/Views/Shared/{0}.cshtml", 

            //Admin
            "~/Administration/Views/{1}/{0}.cshtml",
            "~/Administration/Views/Shared/{0}.cshtml",
            };

            FileExtensions = new[] { "cshtml" };
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, masterPath, true, fileExtensions);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, null, false, fileExtensions);
        }
    }
}
