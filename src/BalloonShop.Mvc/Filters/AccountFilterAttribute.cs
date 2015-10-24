using System;
using System.Web.Mvc;
using StructureMap;
using NHibernate;
using BalloonShop.Model;
using System.Security.Principal;
using BalloonShop.Mvc.Helpers;

namespace BalloonShop.Mvc
{
	public class AccountFilterAttribute : ActionFilterAttribute
	{
		private readonly IContainer _container;

		public AccountFilterAttribute ()
		{
			_container = ObjectFactory.Container;
		}

		public override void OnActionExecuting (ActionExecutingContext filterContext)
		{
			var session = _container.GetInstance<ISession>();
			var identity = _container.GetInstance<IIdentity> ();

			if (!identity.IsAuthenticated)
				return;

			filterContext.Controller.ViewBag.Account = session.Get<Account>(IIdentityHelpers.Identity(identity));
		}
	}
}

