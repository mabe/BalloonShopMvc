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
        public static MvcHtmlString Paging<T, TController>(this HtmlHelper helper, object model, Func<int, Expression<Action<TController>>> func)
            where TController : Controller
        {
            var sb = new StringBuilder();
            var list = (PagedList<T>)model;
            if (list.NumberOfPages <= 1) return MvcHtmlString.Empty;
            //var url = new System.Web.Mvc.UrlHelper(helper.ViewContext.RequestContext);
            var url = string.Concat("/", helper.ViewContext.RouteData.Values["controller"], "/", helper.ViewContext.RouteData.Values["action"], !string.IsNullOrEmpty(helper.ViewContext.RouteData.Values["id"].ToString()) ? "/" + helper.ViewContext.RouteData.Values["id"] : string.Empty, "/?page=");


            sb.Append("<div class=\"pagination pagination-centered\"><ul>");

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

            sb.Append("</ul></div>");

            return new MvcHtmlString(sb.ToString());
        }
    }
}
