using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using NHibernate;
using BalloonShop.Model;
using System.ComponentModel.DataAnnotations;
using BalloonShop.Mvc.Models;
using BalloonShop.Mvc.Services;
using BalloonShop.Mvc.Helpers;

namespace BalloonShop.Mvc.Controllers
{
	[ShoppingCartFilter]
	[CatalogFilter]
	[AccountFilter]
    public class AccountController : Controller
    {
		private readonly IIdentity _identity;
		private readonly ISession _session;
		private readonly IAuthenticationService _authenticationService;

		public AccountController(IIdentity identity, ISession session, IAuthenticationService authenticationService)
		{
			_identity = identity;
			_session = session;
			_authenticationService = authenticationService;
		}

        //
        // GET: /Account/

		public ActionResult Register()
        {
			if (_identity.IsAuthenticated)
				return RedirectToAction("Index", "Home");

            return View();
        }

		[HttpPost]
		public ActionResult Register(AccountViewModel model) 
		{
			var emailExsits = _session.QueryOver<Account>().Where(x => x.Email == model.Email).SingleOrDefault() != null;

			if (emailExsits) {
				ModelState.AddModelError("Email", "There is already an account with this Email address.");
			}

			if (!ModelState.IsValid) {
				return View();
			}

			_session.Save(new Account(model.Email, model.Password));

			return Redirect("/Account/Login");
		}

		public ActionResult Login(string ReturnUrl) {
			ViewBag.ReturnUrl = ReturnUrl;

			return View();
		}

		[HttpPost]
		public ActionResult Login(string email, string password, string returnUrl)
		{
			var account = _session.QueryOver<Account>().Where(x => x.Email == email).SingleOrDefault();

			if (account == null || !account.Authenticate(password)) {
				ModelState.AddModelError("", "There is somthing wrong with the given Email or Password.");
			}

			if (!ModelState.IsValid) {
				return View();
			}

			_authenticationService.Authenticate(account.Id, new[] { "Customers" });

			return string.IsNullOrEmpty(returnUrl) ? (ActionResult)Redirect("/") : Redirect(returnUrl);
		}

		public ActionResult Logout() {
			_authenticationService.UnAuthenticate();

			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult Details() {
			var account = _session.Get<Account>(_identity.Identity());

			ViewBag.ShippingRegions = _session.QueryOver<ShippingRegion>().List().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

			return View(new AccountDetailsViewModel(account.Details));
		}

		[Authorize]
		[HttpPost]
		public ActionResult Details(AccountDetailsViewModel model) {
			ViewBag.ShippingRegions = _session.QueryOver<ShippingRegion>().List().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

			if (!ModelState.IsValid) {
				return View();
			}

			var account = _session.Get<Account>(_identity.Identity());

			var details = account.Details ?? (account.Details = new AccountDetails());

			details.Address1 = model.Address1;
			details.Address2 = model.Address2;
			details.City = model.City;
			details.PostalCode = model.PostalCode;
			details.Region = model.Region;
			details.Country = model.Country;
			details.ShippingRegion = model.ShippingRegion;
			details.DaytimePhone = model.DaytimePhone;
			details.EveningPhone = model.EveningPhone;
			details.MobilePhone = model.MobilePhone;
			details.CreditCard = new CreditCard() { 
				CardholderName = model.CardholderName,
 				CardType = model.CardType,
				CardNumber = model.CardNumber,
				IssueDate = model.IssueDate,
				ExpiryDate = model.ExpiryDate,
				IssueNumber = model.IssueNumber
			};

			return RedirectToAction("Details");
		}
    }
}
