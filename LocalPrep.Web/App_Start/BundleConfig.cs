using System.Web;
using System.Web.Optimization;

namespace LocalPrep.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/js/jquery.js",
                      "~/js/firebase.js",
                      "~/js/plugins.js",
                      "~/js/functions.js",
                      "~/js/jquery.payform.min.js",
                      "~/js/jquery.timepicker.min.js",                     
                      "~/js/lightslider.js"));            

                  bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/css/plugins.css",
                      "~/css/style.css",
                      "~/css/custom.css",
                      "~/css/lightslider.css"));
        }
    }
}
