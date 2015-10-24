using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using BalloonShop.Model;
using BalloonShop.Mvc.Helpers;
using System.Security.Principal;
using BalloonShop.Messages;
using Rhino.ServiceBus;
using BalloonShop.Mvc.Features.Cart;
using BalloonShop.Mvc.Features.Account;


namespace BalloonShop.Mvc.Features.Cart
{
	[ShoppingCartFilter]
	[CatalogFilter]
	[AccountFilter]
	public class UiController : Controller
	{
		private readonly ISession _session;

		public UiController(ISession session)
		{
			_session = session;
		}

		[HttpPost]
		public ActionResult Add(Add.Command command)
		{
			new Add.Handler(_session).Execute(command);

			if (!string.IsNullOrEmpty(command.returnurl)) {
				return Redirect(command.returnurl);
			}

			return RedirectToAction("Show", "Balloon", new { id = command.balloonId });
		}

		[HttpPost]
		public ActionResult Remove(string customerCartId, int remove) {
			_session.Delete(_session.Load<ShoppingCart>(new ShoppingCart() { CartId = customerCartId, Product = _session.Load<Product>(remove) }));

			return Redirect("/Cart");
		}

		[HttpPost]
		public ActionResult Update(string customerCartId, IDictionary<int, int> items) {
			var cart = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();

			foreach (var item in items)
			{
				cart.Single(x => x.Product.Id == item.Key).Quantity = item.Value;
			}

			return Redirect("/Cart");
		}

		public ActionResult Index(string customerCartId) {
			var model = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();

			ViewBag.Total = model.Sum(x => x.Product.Price * x.Quantity);
			ViewBag.HideCartNavigation = true;

			return View(model);
		}
	}
}

