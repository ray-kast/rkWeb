using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace rkWeb.Bundling {
  public class BundleWrapper {
    private Bundle bundle;

    public BundleWrapper(Bundle bundle) {
      this.bundle = bundle;
    }

    public BundleWrapper File(params string[] files) {
      bundle.Include(files);
      return this;
    }

    public BundleWrapper Dir(string dir, string pattern, bool subdirs = false) {
      bundle.IncludeDirectory(dir, pattern, subdirs);
      return this;
    }

    public BundleWrapper Dirs(string pattern, bool subdirs = false, params string[] dirs) {
      foreach (string dir in dirs) Dir(dir, pattern, subdirs);
      return this;
    }

    public BundleWrapper Dirs(bool subdirs = false, params KeyValuePair<string, string>[] dirs) {
      foreach (var pair in dirs) Dir(pair.Key, pair.Value, subdirs);
      return this;
    }
  }
}