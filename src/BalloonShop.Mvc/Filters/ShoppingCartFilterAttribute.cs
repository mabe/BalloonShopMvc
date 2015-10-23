using System;
using System.Linq;
using System.Web.Mvc;
using StructureMap;
using NHibernate;
using BalloonShop.Model;
using BalloonShop.Queries;

namespace BalloonShop.Mvc
{
	public class ShoppingCartFilterAttribute : ActionFilterAttribute
	{
		public ShoppingCartFilterAttribute ()
		{
			
		}

		public override void OnActionExecuting (ActionExecutingContext filterContext)
		{
			var session = ObjectFactory.Container.GetInstance<ISession>();
			var customerCartId = filterContext.HttpContext.Request.Cookies["customerCartId"].Value;
			var model = session.ShoppingCartByCartId(customerCartId).List();

			filterContext.Controller.ViewBag.Cart = model;
			filterContext.Controller.ViewBag.Total = model.Sum(x => x.Product.Price * x.Quantity);
		}
	}
}

