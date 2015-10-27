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
		private readonly CommandQueryHandler _commandHandler;

		public UiController(CommandQueryHandler commandHandler)
		{
			_commandHandler = commandHandler;
		}

		[HttpPost]
		public ActionResult Add(Add.Command command)
		{
			_commandHandler.ExecuteCommand (command);

			if (!string.IsNullOrEmpty(command.returnurl)) {
				return Redirect(command.returnurl);
			}

			return RedirectToAction("Show", "Balloon", new { id = command.balloonId });
		}

		[HttpPost]
		public ActionResult Remove(Remove.Command command) 
		{
			_commandHandler.ExecuteCommand (command);

			return Redirect("/Cart");
		}

		[HttpPost]
		public ActionResult Update(Update.Command command) 
		{
			_commandHandler.ExecuteCommand (command);

			return Redirect("/Cart");
		}

		public ActionResult Index(Index.Query query) {
			var model = _commandHandler.ExecuteQuery<Index.Query, Index.Response> (query);

			ViewBag.HideCartNavigation = model.HideCartNavigation;

			return View(model);
		}
	}
}

