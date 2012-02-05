using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
	public class OrderDetail
	{
		public virtual Order Order { get; set; }
		public virtual int ProductId { get; set; }
		public virtual string ProductName { get; set; }
		public virtual int Quantity { get; set; }
		public virtual decimal UnitCost { get; set; }
		public virtual decimal Subtotal { get; protected set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			var t = obj as OrderDetail;
			if (t == null)
				return false;
			if (Order.Id == t.Order.Id && ProductId == t.ProductId)
				return true;
			return false;
		}
		public override int GetHashCode()
		{
			return (Order.Id + "|" + ProductId).GetHashCode();
		}  
	}

	public class OrderDetailMap : ClassMap<OrderDetail>
	{
		public OrderDetailMap()
		{
			CompositeId().KeyProperty(x => x.ProductId, "ProductId").KeyReference(x => x.Order, "OrderId");
			Map(x => x.ProductName);
			Map(x => x.Quantity);
			Map(x => x.UnitCost);
			Map(x => x.Subtotal);
		}
	}
}
