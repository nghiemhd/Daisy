using System.Web.Mvc;

namespace Daisy.AdminTest
{
    public class AdminTestAreaRegistration: AreaRegistration 
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
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = "Admin", id = "" },
                new[] { "Daisy.AdminTest.Controllers" }
            );
        }
    }
}