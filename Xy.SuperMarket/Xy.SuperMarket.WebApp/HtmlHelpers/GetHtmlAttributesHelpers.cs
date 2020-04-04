using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Xy.SuperMarket.WebApp.HtmlHelpers
{
    public static class GetHtmlAttributesHelpers
    {
        /// <summary>
        /// Gets an object containing a htmlAttributes collection for any Razor HTML helper component,
        /// supporting a static set (anonymous object) and/or a dynamic set (Dictionary)
        /// </summary>
        /// <param name="fixedHtmlAttributes">A fixed set of htmlAttributes (anonymous object)</param>
        /// <param name="dynamicHtmlAttributes">A dynamic set of htmlAttributes (Dictionary)</param>
        /// <returns>A collection of htmlAttributes including a merge of the given set(s)</returns>
        public static IDictionary<string, object> GetHtmlAttributes(
            object fixedHtmlAttributes = null,
            IDictionary<string, object> dynamicHtmlAttributes = null
            )
        {
            var rvd = (fixedHtmlAttributes == null)
                ? new RouteValueDictionary()
                : HtmlHelper.AnonymousObjectToHtmlAttributes(fixedHtmlAttributes);
            if (dynamicHtmlAttributes != null)
            {
                foreach (KeyValuePair<string, object> kvp in dynamicHtmlAttributes)
                    rvd[kvp.Key] = kvp.Value;
            }
            return rvd;
        }
    }
}