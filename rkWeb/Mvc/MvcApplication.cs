using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace rkWeb.Mvc {
  public delegate void FilterRegistration(GlobalFilterCollection filters);

  public delegate void RouteRegistration(RouteCollection routes);

  public delegate void BundleRegistration(BundleCollection bundles);

  public abstract class rkWebApplication : HttpApplication {
    private static readonly NullOrderer nullOrderer = new NullOrderer();

    private static readonly CssMinify cssMinify = new CssMinify();

    private static readonly JsMinify jsMinify = new JsMinify();

    protected void Application_Start() {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(RegisterWebApiConfig);
      RegisterFilters(GlobalFilters.Filters);
      RegisterRoutes(RouteTable.Routes);
      RegisterBundlesInternal(BundleTable.Bundles);
      RegisterBundles(BundleTable.Bundles);
    }

    public event Action<HttpConfiguration> RegisterWebApiConfig;

    public event FilterRegistration RegisterFilters;

    public event RouteRegistration RegisterRoutes;

    public event BundleRegistration RegisterBundles;

    protected CustomStyleBundle MakeStyleBundle() {
      throw new NotImplementedException();
    }

    private void RegisterBundlesInternal(BundleCollection bundles) {
      bundles.Add(new CustomStyleBundle("~/Styles/Fonts")
        .IncludeDirectory("~/Res/Fonts/", "Fonts.scss", true)
        .Include("~/Content/font-awesome.css"));
    }
  }
}