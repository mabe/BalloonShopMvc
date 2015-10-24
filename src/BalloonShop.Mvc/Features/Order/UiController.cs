using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Principal;
using NHibernate;
using BalloonShop.Model;
using BalloonShop.Mvc.Features.Account;
using BalloonShop.Mvc.Helpers;

namespace BalloonShop.Mvc.Features.Order
{
	[ShoppingCartFilter]
	[CatalogFilter]
	[AccountFilter]
	public class UiController : Controller
	{
		private readonly IIdentity _identity;
		private readonly ISession _session;

		public UiController(IIdentity identity, ISession session)
		{
			_identity = identity;
			_session = session;
		}

		[Authorize]
		public ActionResult Checkout(string customerCartId) {
			var cart = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();
			var account = _session.Get<Model.Account>(_identity.Identity());
			var region = account.Details != null ? account.Details.ShippingRegion : 0;

			ViewBag.Cart = cart;
			ViewBag.Total = cart.Sum(x => x.Product.Price * x.Quantity);
			ViewBag.ShippingRegions = _session.QueryOver<ShippingRegion>().List().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == region }).ToList();
			ViewBag.ShippingTypes = _session.QueryOver<Shipping>().List();
			ViewBag.HideCartNavigation = true;

			return View(new CheckoutViewModel() { AccountDetails = new AccountDetailsViewModel(account.Details) });
		}

		[Authorize]
		[HttpPost]
		public ActionResult Checkout(string customerCartId, CheckoutViewModel model)
		{
			var cart = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();
			var account = _session.Get<Model.Account>(_identity.Identity());

			ViewBag.Cart = cart;
			ViewBag.Total = cart.Sum(x => x.Product.Price * x.Quantity);
			ViewBag.ShippingRegions = _session.QueryOver<ShippingRegion>().List().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == account.Details.ShippingRegion }).ToList();
			ViewBag.ShippingTypes = _session.QueryOver<Shipping>().List();

			if (!ModelState.IsValid)
				return View();

			int taxId;
			switch (model.AccountDetails.ShippingRegion)
			{
			case 2:
				taxId = 1;
				break;
			default:
				taxId = 2;
				break;
			}

			var order = new Model.Order(account.Id,
				"",
				account.Email,
				cart.Select(item => new OrderDetail() {
					ProductId = item.Product.Id,
					ProductName = item.Product.Name,
					Quantity = item.Quantity,
					UnitCost = item.Product.Price
				}).ToList(),
				_session.Get<Tax>(taxId),
				_session.Get<Shipping>(model.ShippingType));

			foreach (var item in cart) {
				_session.Delete(item);
			}

			_session.Save(order);

			//_bus.Send(new InitialNotificationMessage() { OrderId = order.Id, CorrelationId = order.SagaCorrelationId });

			return Redirect("/Order/Placed");
		}

		public ActionResult Placed()
		{
			ViewBag.HideCartNavigation = true;

			return View();
		}
	}
}

