using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;
using System.Security.Principal;
using BalloonShop.Mvc.Services;

namespace BalloonShop.Mvc.Config
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            For<IControllerActivator>().Use<StructureMapControllerActivator>();
			For<IIdentity>().Use(x => HttpContext.Current.User.Identity);
			For<IAuthenticationService>().Use<FormsAuthenticationService>();
			For<IControllerFactory> ().Use<ControllerFactory> ();

			For<ICommandHandler<Features.Cart.Remove.Command>> ().Use<Features.Cart.Remove.Handler> ();
			For<ICommandHandler<Features.Cart.Add.Command>> ().Use<Features.Cart.Add.Handler> ();
			For<ICommandHandler<Features.Cart.Update.Command>> ().Use<Features.Cart.Update.Handler> ();
			For<IQueryHandler<Features.Cart.Index.Query, Features.Cart.Index.Response>> ().Use<Features.Cart.Index.Handler> ();
        }
    }
}