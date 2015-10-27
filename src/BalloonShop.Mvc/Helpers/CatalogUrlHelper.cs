using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Web.Mvc;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Helpers
{
    public static class CatalogUrlHelper
    {
    //    public static string Action<TController>(this System.Web.Mvc.UrlHelper url, Expression<Action<TController>> action) where TController : Controller
    //    {
    //        var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(action);
    //        return url.RouteUrl(routeValues);
    //    }   

		public static string DeFilter(string name) {
			return name.Replace ("-and-", "&");
		}

		private static string Filter(string name) {
			return name.Replace ("&", "-and-");
		}

		public static string DepartmentUrl(this HtmlHelper html, Department department){
			return "/catalog/" + HttpUtility.UrlEncode (Filter(department.Name));
		}

		public static string CategoryUrl(this HtmlHelper html, Category category) {
			return "/catalog/" 
				+ HttpUtility.UrlEncode (Filter(category.Department.Name))
				+ "/"
				+ HttpUtility.UrlEncode (Filter(category.Name));
		}

		public static string ProductUrl(this HtmlHelper html, Product product) {
			var department = (string)html.ViewContext.RouteData.Values ["department"] 
				?? (product.Categories.Any() ? product.Categories.First ().Department.Name : "");

			var category = (string)html.ViewContext.RouteData.Values ["category"] 
				?? (product.Categories.Any() ? product.Categories.First ().Name : "");

			return "/catalog/" 
				+ HttpUtility.UrlEncode (Filter(department))
				+ "/"
				+ HttpUtility.UrlEncode (Filter(category))
				+ "/"
				+ HttpUtility.UrlEncode (Filter(product.Name));


		}
    }
}
