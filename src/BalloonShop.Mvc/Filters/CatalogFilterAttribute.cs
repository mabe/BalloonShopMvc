using System;
using System.Linq;
using System.Web.Mvc;
using StructureMap;
using NHibernate;
using BalloonShop.Model;
using BalloonShop.Queries;

namespace BalloonShop.Mvc
{
	public class CatalogFilterAttribute : ActionFilterAttribute
	{
		public CatalogFilterAttribute ()
		{
		}

		public override void OnActionExecuting (ActionExecutingContext filterContext)
		{
			var session = ObjectFactory.Container.GetInstance<ISession>();

			var departmentId = filterContext.HttpContext.Request.QueryString ["departmentId"];
			var categoryId = filterContext.HttpContext.Request.QueryString ["categoryId"];

			filterContext.Controller.ViewBag.Departments = session.QueryOver<Department>().List();
			filterContext.Controller.ViewBag.Categories = string.IsNullOrEmpty(departmentId) 
				? Enumerable.Empty<Category> ()
				: session.CategoriesInDepartment(int.Parse(departmentId)).List();
			
			filterContext.Controller.ViewBag.DepartmentId = 
				string.IsNullOrEmpty(departmentId) ? (int?)null : int.Parse(departmentId);
			filterContext.Controller.ViewBag.CategoryId = 
				string.IsNullOrEmpty(categoryId) ? (int?)null : int.Parse(categoryId);
		}
	}
}

