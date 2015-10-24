using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model {
	public class ShippingRegion {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
	}

	public class ShippingRegionMap : ClassMap<ShippingRegion> {
		public ShippingRegionMap() {
			Table ("shippingregion");
			Id(x => x.Id).GeneratedBy.Identity().Column("ShippingRegionID");
			Map(x => x.Name).Column("ShippingRegion");
		}
	}
}
