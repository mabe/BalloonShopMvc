using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
	public class AccountDetails
	{
		public virtual string Address1 { get; set; }
		public virtual string Address2 { get; set; }
		public virtual string City { get; set; }
		public virtual string Region { get; set; }
		public virtual string PostalCode { get; set; }
		public virtual string Country { get; set; }
		public virtual int ShippingRegion { get; set; }
		public virtual string DaytimePhone { get; set; }
		public virtual string EveningPhone { get; set; }
		public virtual string MobilePhone { get; set; }
	}

	
}
