using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Mvc.Models
{
	public class AccountViewModel
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}