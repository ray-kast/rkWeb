using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace rkWeb.Bundling {
  public class BundleDictionary {
    protected readonly NullOrderer nullOrderer = new NullOrderer();

    protected readonly CssMinify cssMinify = new CssMinify();

    protected readonly JsMinify jsMinify = new JsMinify();

    protected readonly CustomBundleResolver bundleResolver = new CustomBundleResolver();

    protected Dictionary<string, Bundle> bundles = new Dictionary<string, Bundle>();

    public Bundle[] Bundles { get { return bundles.Values.ToArray(); } }

    public BundleDictionary() {
      BundleResolver.Current = bundleResolver;
    }

    public BundleDictionary Add(Bundle bundle) {
      if (bundles.ContainsKey(bundle.Path)) bundles[bundle.Path] = bundle;
      else bundles.Add(bundle.Path, bundle);
      return this;
    }

    public BundleDictionary Remove(string vPath) {
      bundles.Remove(vPath);
      return this;
    }

    public BundleWrapper Style(string vPath) {
      CustomStyleBundle bundle = new CustomStyleBundle(vPath);
      bundle.Orderer = nullOrderer;
      bundle.Transforms.Add(cssMinify);
      Add(bundle);
      return new BundleWrapper(bundle);
    }

    public BundleWrapper Script(string vPath) {
      CustomScriptBundle bundle = new CustomScriptBundle(vPath);
      bundle.Orderer = nullOrderer;
      bundle.Transforms.Add(jsMinify);
      Add(bundle);
      return new BundleWrapper(bundle);
    }

    public void Dump() {
      foreach (Bundle bundle in Bundles) {
        BundleTable.Bundles.Add(bundle);
      }
    }

    public void Optimize(bool enable = true) {
      BundleTable.EnableOptimizations = enable;
    }
  }
}