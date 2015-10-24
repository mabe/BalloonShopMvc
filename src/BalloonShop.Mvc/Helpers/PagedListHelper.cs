using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BalloonShop.Infrastructure;
using System.Linq.Expressions;
using System.Text;

namespace BalloonShop.Mvc.Helpers
{
    public static class PagedListHelper
    {
        public static MvcHtmlString Paging<T>(this HtmlHelper helper, object model, Func<string> func)
        {
            var sb = new StringBuilder();
            var list = (PagedList<T>)model;
            if (list.NumberOfPages <= 1) return MvcHtmlString.Empty;

            //var url = new System.Web.Mvc.UrlHelper(helper.ViewContext.RequestContext);
			var url = func();
			url = string.Concat (url, url.Contains("?") ? "&" : "/?", "page=");

            sb.Append("<ul class=\"pagination pagination-centered\">");

            if (list.HasPreviousPage) {
                sb.AppendFormat("<li><a href=\"{0}\">Prev</a></li>", url + (list.Page - 1));
            } else {
                sb.Append("<li><span>Prev</span></li>");
            }

            sb.AppendFormat("<li><span>Page {0} of {1}</span></li>", list.Page, list.NumberOfPages);

            if (list.HasNextPage) {
                sb.AppendFormat("<li><a href=\"{0}\">Next</a></li>", url + (list.Page + 1));
            } else {
                sb.Append("<li><span>Next</span></li>");
            }

            sb.Append("</ul>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}
