using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model {
	public class Shipping {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual decimal Cost { get; set; }

		public virtual ShippingRegion Region { get; set; }
	}
	/*
	public class ShippingMap : ClassMap<Shipping> {
		public ShippingMap() {
			Id(x => x.Id).GeneratedBy.Identity().Column("ShippingID");
			Map(x => x.Name).Column("ShippingType");
			Map(x => x.Cost).Column("ShippingCost");

			References(x => x.Region, "ShippingRegionID");
		}
	}
	*/
}
