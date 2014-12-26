using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Autoprefixer.PostProcessors;
using rkWeb.Bundling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Collections;

namespace rkWeb.Mvc {
  public delegate void FilterRegistration(GlobalFilterCollection filters);

  public delegate void RouteRegistration(RouteCollection routes);

  public delegate void BundleRegistration(BundleDictionary bundles);

  public class rkWebApplication : HttpApplication {
    private BundleDictionary bundleDictionary = new BundleDictionary();

    public event Action<HttpConfiguration> RegisterWebApiConfig;

    public event FilterRegistration RegisterFilters;

    public event RouteRegistration RegisterRoutes;

    public event BundleRegistration RegisterBundles;

    public void Application_Start() {
      AreaRegistration.RegisterAllAreas();
      if (RegisterWebApiConfig != null) GlobalConfiguration.Configure(RegisterWebApiConfig);
      if (RegisterFilters != null) RegisterFilters(GlobalFilters.Filters);
      if (RegisterRoutes != null) RegisterRoutes(RouteTable.Routes);
      if (RegisterBundles != null) RegisterBundles(bundleDictionary);
      bundleDictionary.Dump();
    }

    protected BundleWrapper BundleFonts(BundleDictionary bundles) {
      return bundles.Style("~/Styles/Fonts")
        .Dir("~/Res/Fonts/", "Fonts.scss", true)
        .File("~/Content/font-awesome.css");
    }

    protected BundleWrapper BundleDefaultScripts(BundleDictionary bundles) {
      return bundles.Script("~/Scripts/Default")
        .File("~/Scripts/modernizr-{version}.js",
          "~/Res/Scripts/Lib/rkLib.ts")
        .Dir("~/Res/Scripts/Lib/rkLib", "*.ts", true);
    }
  }
}