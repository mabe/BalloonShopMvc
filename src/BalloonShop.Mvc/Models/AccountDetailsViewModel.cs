using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BalloonShop.Model;
using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Mvc.Models
{
	public class AccountDetailsViewModel
	{
		public AccountDetailsViewModel()
		{

		}

		public AccountDetailsViewModel(AccountDetails details)
		{
			if (details == null)
				return;

			Address1 = details.Address1;
			Address2 = details.Address2;
			City = details.City;
			PostalCode = details.PostalCode;
			ShippingRegion = details.ShippingRegion;
			Region = details.Region;
			Country = details.Country;
			DaytimePhone = details.DaytimePhone;
			EveningPhone = details.EveningPhone;
			MobilePhone = details.MobilePhone;
		}

		[Required]
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		[Required]
		public string City { get; set; }
		public string Region { get; set; }
		[Required]
		public string PostalCode { get; set; }
		public string Country { get; set; }
		[Required]
		public int ShippingRegion { get; set; }
		public string DaytimePhone { get; set; }
		public string EveningPhone { get; set; }
		public string MobilePhone { get; set; }
	}
}