using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daisy.Web.Extensions
{
    public static class HtmlExtensions
    {
        public static string ActivePage(this HtmlHelper helper, string controller, string action)
        {
            string classValue = "";
            string currentController = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();
            string currentAction = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();

            if (string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase))
            {
                classValue = "selected";
            }

            return classValue;
        }
    }
}