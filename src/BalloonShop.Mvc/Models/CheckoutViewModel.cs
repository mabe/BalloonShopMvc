using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Mvc.Models {
	public class CheckoutViewModel {
		[Required]
		public string Address1 { get; set; }
		[Required]
		public string Address2 { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string Region { get; set; }
		[Required]
		public string ZipCode { get; set; }
		[Required]
		public string Country { get; set; }
		[Range(2, 4)]
		public int ShippingRegion { get; set; }
		public string DaytimePhone { get; set; }
		public string MobilePhone { get; set; }
		public string Email { get; set; }
		public int ShippingType { get; set; }
	}
}
