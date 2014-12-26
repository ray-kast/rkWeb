using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace rkWeb.Mvc {
  public abstract class rkWebViewPage<TModel> : WebViewPage<TModel> {
    public delegate HelperResult Markup(object templateWriter);

    protected static void NullFunc() { }

    public void RedefineSection(string name, bool required = true) {
      if (this.IsSectionDefined(name)) this.DefineSection(name, () => { this.RenderSection(name); });
      else if (required) this.DefineSection(name, NullFunc);
    }

    public void SuffixTitle(string suffix, string join = " \u2014 ") {
      string title = ViewBag.Title as string;
      ViewBag.Title = title + (string.IsNullOrEmpty(title) ? "" : join) + suffix;
    }

    public string Header { get { return ViewBag.Header as string ?? ViewBag.Title as string; } }
  }
}