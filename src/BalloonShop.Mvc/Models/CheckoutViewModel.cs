using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Mvc.Models {
	public class CheckoutViewModel {
		public AccountDetailsViewModel AccountDetails { get; set; }
		[Range(1, 50)]
		public int ShippingType { get; set; }
	}
}
