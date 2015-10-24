using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BalloonShop.Mvc
{
	public class ControllerFactory : DefaultControllerFactory
	{
		protected override Type GetControllerType(RequestContext requestContext, string controllerName)
		{
			var name = "BalloonShop.Mvc.Features." + controllerName + ".UiController";
			return
				typeof (ControllerFactory).Assembly.GetType(name);
		}
	}
}

