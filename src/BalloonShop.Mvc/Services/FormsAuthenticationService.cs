using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;

namespace BalloonShop.Mvc.Services
{
	public class FormsAuthenticationService : IAuthenticationService
	{
		public void Authenticate(object identity, string[] roles)
		{
			var ticket = new FormsAuthenticationTicket(1, identity.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Join(",", roles));

			HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket)));
		}

		public void UnAuthenticate()
		{
			FormsAuthentication.SignOut();
		}

		public static void AuthenticateRequest(object sender, EventArgs e) {
			var authCookie =  HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie != null)
			{
				var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

				HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), authTicket.UserData.Split(new Char[] { ',' }));
			}
		}
	}
}