using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BalloonShop.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BalloonShop.Mvc.Features.Account
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

			var card = details.CreditCard;

			if (card != null)
			{
				CardholderName = card.CardholderName;
				CardType = card.CardType;
				CardNumber = card.CardNumber;
				IssueDate = card.IssueDate;
				ExpiryDate = card.ExpiryDate;
				IssueNumber = card.IssueNumber;
			}
		}

		[Required]
		[DisplayName("Address line 1")]
		public string Address1 { get; set; }
		[DisplayName("Address line 2")]
		public string Address2 { get; set; }
		[Required]
		[DisplayName("City")]
		public string City { get; set; }
		[Required]
		[DisplayName("Region")]
		public string Region { get; set; }
		[Required]
		[DisplayName("Zip / Postal Code")]
		public string PostalCode { get; set; }
		[Required]
		[DisplayName("Country")]
		public string Country { get; set; }
		[Range(2, 4)]
		[DisplayName("Shipping Region")]
		public int ShippingRegion { get; set; }
		[DisplayName("Daytime Phone no")]
		public string DaytimePhone { get; set; }
		[DisplayName("Evening Phone no")]
		public string EveningPhone { get; set; }
		[DisplayName("Mobile Phone no")]
		public string MobilePhone { get; set; }

		public string CardholderName { get; set; }

		public string CardType { get; set; }

		public string CardNumber { get; set; }

		public string IssueDate { get; set; }

		public string ExpiryDate { get; set; }

		public string IssueNumber { get; set; }
	}
}