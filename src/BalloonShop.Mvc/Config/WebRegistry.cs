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
        }
    }
}