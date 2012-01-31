using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BalloonShop.Mvc.Models {
	public class CheckoutViewModel {
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string ZipCode { get; set; }
		public string Country { get; set; }
		public int ShippingRegion { get; set; }
		public string DaytimePhone { get; set; }
		public string MobilePhone { get; set; }
		public string Email { get; set; }
		public int ShippingType { get; set; }
	}
}
