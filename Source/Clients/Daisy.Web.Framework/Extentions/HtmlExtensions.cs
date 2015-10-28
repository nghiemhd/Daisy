using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Daisy.Common.Extensions;

namespace Daisy.Web.Framework.Extentions
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

        public static string If(this HtmlHelper helper, bool condition, string then, string @else = "")
        {
            if (condition)
            {
                return then;
            }
            else
            {
                if (!@else.IsNullOrEmpty())
                {
                    return @else;
                }
            }

            return string.Empty;
        }

        public static string RequireScript(this HtmlHelper html, RenderOptions option, string path, int priority = 1)
        {
            var requiredScripts = HttpContext.Current.Items[option.ToString()] as List<ResourceInclude>;
            if (requiredScripts == null) HttpContext.Current.Items[option.ToString()] = requiredScripts = new List<ResourceInclude>();
            if (!requiredScripts.Any(i => i.Path == path)) requiredScripts.Add(new ResourceInclude() { Path = path, Priority = priority });
            return null;
        }

        public static HtmlString RenderRequiredScripts(this HtmlHelper html, RenderOptions option)
        {
            var requiredScripts = HttpContext.Current.Items[option.ToString()] as List<ResourceInclude>;
            if (requiredScripts == null) return null;
            StringBuilder sb = new StringBuilder();
            foreach (var item in requiredScripts.OrderByDescending(i => i.Priority))
            {
                sb.AppendFormat("<script src=\"{0}\" type=\"text/javascript\"></script>\n", item.Path);
            }
            return new HtmlString(sb.ToString());
        }        
    }

    public enum RenderOptions
    { 
        Head,
        Body
    }

    public class ResourceInclude
    {
        public string Path { get; set; }
        public int Priority { get; set; }
    }
}
