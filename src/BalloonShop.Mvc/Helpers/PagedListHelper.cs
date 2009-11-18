using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BalloonShop.Infrastructure;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Web.Mvc;

namespace BalloonShop.Mvc.Helpers
{
    public static class PagedListHelper
    {
        public static string Paging<T, TController>(this HtmlHelper helper, object model, Func<int, Expression<Action<TController>>> func) 
            where TController : Controller
        {
            var sb = new StringBuilder();
            var list = (PagedList<T>)model;
            if (list.NumberOfPages < 1) return string.Empty;
            var url = new System.Web.Mvc.UrlHelper(helper.ViewContext.RequestContext);

            sb.AppendFormat("Page {0} of {1}&nbsp;&nbsp;", list.Page, list.NumberOfPages);
            if (list.HasPreviousPage)
                sb.AppendFormat("<a href=\"{0}\">Prev</a>&nbsp;&nbsp;", url.Action<TController>(func(list.Page - 1)));

            if (list.HasNextPage)
                sb.AppendFormat("<a href=\"{0}\">Next</a>", url.Action<TController>(func(list.Page + 1)));

            return sb.ToString();
        }
    }
}
