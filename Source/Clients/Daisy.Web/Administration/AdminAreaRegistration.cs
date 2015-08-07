using Daisy.Admin.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Daisy.Admin
{
    public class AdminAreaRegistration: AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            RouteConfig.RegisterRoutes(context);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}