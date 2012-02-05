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

namespace BalloonShop.Mvc.Controllers
{
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

			return RedirectToAction("Login");
		}

		[ChildActionOnly]
		public ActionResult Navigation() {
			if (!_identity.IsAuthenticated)
				return View("Navigation_NotAuthenticated");

			var account = _session.Get<Account>(int.Parse(_identity.Name));

			return View(account);
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

			return string.IsNullOrEmpty(returnUrl) ? (ActionResult)RedirectToAction("Index", "Home") : Redirect(returnUrl);
		}

		public ActionResult Logout() {
			_authenticationService.UnAuthenticate();

			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult Details() {
			var account = _session.Get<Account>(int.Parse(_identity.Name));

			return View(account.Details);
		}
    }
}
