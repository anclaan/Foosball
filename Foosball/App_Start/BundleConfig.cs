﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace Foosball
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/kendo/kendo.all.min.js",
                // "~/Scripts/kendo/kendo.timezones.min.js", // uncomment if using the Scheduler
                "~/Scripts/kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.default.min.css"));


            bundles.IgnoreList.Clear();
        }
    }
}