using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.Web.Mvc.Internal;

namespace BalloonShop.Mvc.Helpers
{
    public static class UrlHelper
    {
        public static string Action<TController>(this System.Web.Mvc.UrlHelper url, Expression<Action<TController>> action) where TController : Controller
        {
            var routeValues = ExpressionHelper.GetRouteValuesFromExpression(action);
            return url.RouteUrl(routeValues);
        }   
    }
}
