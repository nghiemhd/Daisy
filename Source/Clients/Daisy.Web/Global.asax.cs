using Daisy.Common;
using Daisy.Web.Framework.ViewEngines.Razor;
using Daisy.Web.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Daisy.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Utilities.EncryptData();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings();

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
