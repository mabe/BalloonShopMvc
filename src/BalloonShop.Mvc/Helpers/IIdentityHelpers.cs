using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace BalloonShop.Mvc.Helpers
{
	public static class IIdentityHelpers
	{
		public static Guid Identity(this IIdentity identity) {
			return Guid.Parse(identity.Name);
		}
	}
}