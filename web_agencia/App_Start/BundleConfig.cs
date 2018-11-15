using System.Web;
using System.Web.Optimization;

namespace web_agencia
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            /*ESTILOS Y JAVASCRIPT DASHBOARD*/
            bundles.Add(new StyleBundle("~/Content/dashboard-pro").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/now-ui-dashboard.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard-pro").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/js/core/popper.min.js",
                       "~/Scripts/js/core/bootstrap-material-design.min.js",
                       "~/Scripts/js/plugins/perfect-scrollbar.jquery.min.js",
                       "~/Scripts/pages/all.js"));

            /*FIN ESTILOS Y JAVASCRIPT DASHBOARD*/

        }
    }
}
