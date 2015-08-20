using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Daisy.Admin.Infrastructure
{
    internal class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/admin/jquery").Include(
                        "~/Administration/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Administration/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/admin/modernizr").Include(
                        "~/Administration/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/admin/bootstrap").Include(
                      "~/Administration/Scripts/bootstrap.js",
                      "~/Administration/Scripts/bootstrap-dialog.js",
                      "~/Administration/Scripts/hotfixBootstrapModal.js",
                      "~/Administration/Scripts/toastr.js",
                      "~/Administration/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/admin/css").Include(
                      "~/Administration/Content/bootstrap.css",
                      "~/Administration/Content/bootstrap-dialog.css",
                      "~/Administration/Content/toastr.css",
                      "~/Administration/Content/daisy.admin.css",
                      "~/Administration/Content/site.css"));
        }
    }
}