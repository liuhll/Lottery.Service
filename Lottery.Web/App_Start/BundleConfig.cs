using System.Web.Optimization;

namespace Lottery.Web
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/plato/css/bootstrap.css",
                "~/plato/css/bootstrap-responsive.css",
                "~/plato/css/docs.css",
                "~/plato/css/prettyPhoto.css",
                "~/plato/js/google-code-prettify/prettify.css",
                "~/plato/css/flexslider.css",
                "~/plato/css/headerfix.css",
                "~/plato/css/refineslide.css",
                "~/plato/css/font-awesome.css",
                "~/plato/css/animate.css",
                "~/plato/css/style.css",
                "~/plato/color/default.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/plato").Include(
                      "~/plato/js/jquery.js",
                      "~/plato/js/modernizr.js",
                      "~/plato/js/jquery.easing.1.3.js",
                      "~/plato/js/google-code-prettify/prettify.js",
                      "~/plato/js/bootstrap.js",
                      "~/plato/js/jquery.prettyPhoto.js",
                      "~/plato/js/portfolio/jquery.quicksand.js",
                      "~/plato/js/portfolio/setting.js",
                      "~/plato/js/hover/jquery-hover-effect.js",
                      "~/plato/js/jquery.flexslider.js",
                      "~/plato/js/classie.js",
                      "~/plato/js/cbpAnimatedHeader.min.js",
                      "~/plato/js/jquery.refineslide.js",
                      "~/plato/js/jquery.ui.totop.js",
                      "~/plato/js/custom.js"
                      ));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}