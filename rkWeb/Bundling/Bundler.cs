using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using rkWeb.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Xml;
using System.Xml.Linq;

namespace rkWeb.Bundles {
  public static class Bundler {
    private const string xmlns = "rkWeb:BundleList";

    public static void MakeBundles(BundleCollection bundles, string url, HttpServerUtility server) {
      CssMinify cssMinify = new CssMinify();
      JsMinify jsMinify = new JsMinify();
      NullOrderer nullOrderer = new NullOrderer();
      XDocument xDoc = XDocument.Load(server.MapPath(url));

      if(xDoc.Root.HasAttribute("forceOptimizations")) BundleTable.EnableOptimizations = bool.Parse(xDoc.Root.AttributeValue("forceOptimizations"));

      var els = from el in xDoc.Root.Elements(XName.Get("bundle", xmlns))
                let type = el.AttributeValue("type")
                where type != null
                where el.HasAttribute("path")
                select el;

      foreach(XElement el in els) {
        Bundle bundle;
        string path = el.AttributeValue("path");
        switch(el.AttributeValue("type")) {
          case "style":
            bundle = new CustomStyleBundle(path);
            bundle.Transforms.Add(cssMinify);
            break;
          case "script":
            bundle = new CustomScriptBundle(path);
            bundle.Transforms.Add(jsMinify);
            break;
          default:
            throw new InvalidOperationException("Type \"" + el.AttributeValue("type") + "\" is invalid.");
        }
        bundles.Add(bundle);
        bundle.Orderer = nullOrderer;

        var includes = from include in el.Elements()
                       where include.Name == XName.Get("file", xmlns) || include.Name == XName.Get("dir", xmlns)
                       select include;

        foreach(XElement include in includes) {
          switch(include.Name.LocalName) {
            case "file":
              bundle.Include(include.AttributeValue("path"));
              break;
            case "dir":
              if(include.HasAttribute("subdirs"))
                bundle.IncludeDirectory(include.AttributeValue("path"), include.AttributeValue("pattern"), bool.Parse(include.AttributeValue("subdirs")));
              else
                bundle.IncludeDirectory(include.AttributeValue("path"), include.AttributeValue("pattern"));
              break;
            default:
              throw new InvalidOperationException("Tag name \"" + el.Name.ToString() + "\" is invalid.");
          }
        }
      }
    }
  }
}