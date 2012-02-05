using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Mvc.Services
{
	public interface IAuthenticationService
	{
		void Authenticate(object identity, string[] roles);
		void UnAuthenticate();
	}
}
