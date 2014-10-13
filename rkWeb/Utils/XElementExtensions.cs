using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace rkWeb.Utils {
  public static class XElementExtensions {
    public static string AttributeValue(this XElement el, XName name) {
      XAttribute attr = el.Attribute(name);
      return attr == null ? null : attr.Value;
    }

    public static bool HasAttribute(this XElement el, XName name) {
      return el.Attribute(name) != null;
    }
  }
}