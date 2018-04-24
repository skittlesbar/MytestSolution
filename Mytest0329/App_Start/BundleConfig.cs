using System.Web;
using System.Web.Optimization;

namespace Mytest0329
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
    "~/Scripts/layout/jquery-1.10.2.js",
    "~/Scripts/layout/jquery.min.js",
    "~/Scripts/layout/jquery.gritter.js",
    "~/Scripts/layout/jquery.nanoscroller.js",
    "~/Scripts/layout/general.js",
    "~/Scripts/layout/jquery-ui.js",
    "~/Scripts/layout/jquery.sparkline.js",
    "~/Scripts/layout/jquery.easy-pie-chart.js",
    "~/Scripts/layout/jquery.nestable.js",
    "~/Scripts/layout/bootstrap-switch.min.js",
    "~/Scripts/layout/bootstrap-datetimepicker.min.js",
    "~/Scripts/layout/select2.min.js",
    "~/Scripts/layout/skycons.js",
    "~/Scripts/layout/bootstrap-slider.js",
    "~/Scripts/layout/intro.js",
    "~/Scripts/layout/voice-commands.js",
    "~/Scripts/layout/bootstrap.min.js",
    "~/Scripts/layout/jquery.flot.js",
    "~/Scripts/layout/jquery.flot.pie.js",
    "~/Scripts/layout/jquery.flot.resize.js",
    "~/Scripts/layout/jquery.flot.labels.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/layout").Include(
                "~/Content/layout/css1.css",
                "~/Content/layout/css2.css",
                "~/Content/layout/css3.css",
                "~/Content/layout/bootstrap-3.3.4.css",
                "~/Content/layout/font-awesome.4.6.0.css",
                "~/Content/layout/jquery.gritter.css",
                "~/Content/layout/nanoscroller.css",
                "~/Content/layout/jquery.easy-pie-chart.css",
                "~/Content/layout/bootstrap-switch.css",
                "~/Content/layout/bootstrap-datetimepicker.min.css",
                "~/Content/layout/select2.css",
                "~/Content/layout/slider.css",
                "~/Content/layout/introjs.css",
                "~/Content/layout/style.css"
               ));
        }
    }
}
