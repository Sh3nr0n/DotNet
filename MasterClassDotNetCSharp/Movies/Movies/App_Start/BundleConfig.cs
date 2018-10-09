using System.Web.Optimization;

namespace Movies
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new StyleBundle("~/bundles/General").Include(
            "~/Content/Site.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Search").Include(
            "~/Scripts/Search.js"
            ));


        }
    }
}