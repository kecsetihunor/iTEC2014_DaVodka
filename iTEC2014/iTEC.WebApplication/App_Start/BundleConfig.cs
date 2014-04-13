using System;
using System.Web.Optimization;

namespace iTEC.WebApplication
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(
              new ScriptBundle("~/Scripts/priorityVendor")
                  .Include("~/Scripts/jquery-{version}.js")
              );

            bundles.Add(
              new ScriptBundle("~/Scripts/vendor")
                  .Include("~/Scripts/jquery.cookie.js")
                  .Include("~/Scripts/jquery.nicescroll.js")
                  .Include("~/Scripts/bootstrap.js")
                  .Include("~/Scripts/bootstrap-slider.js")
                  .Include("~/Scripts/knockout-{version}.js")
                  .Include("~/Scripts/knockout.validation.js")
                  .Include("~/Scripts/upshot.js")
                  .Include("~/Scripts/toastr.js")
                  .Include("~/Scripts/highcharts.js")
              );

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/bootstrap-theme.min.css")
                .Include("~/Content/slider.css")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/core.min.css")
                .Include("~/Content/animations.min.css")
                .Include("~/Content/app.min.css")
                .Include("~/Content/responsive.min.css")
                .Include("~/Content/theme.min.css")
              );
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}