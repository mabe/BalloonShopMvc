using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
	public class Order
	{
		public Order()
		{
			_orderDetails = new List<OrderDetail>();
		}

		public virtual int Id { get; set; }
		
		public virtual string ShippingAddress { get; set; }

		public virtual Guid CustomerId { get; set; }
		public virtual string CustomerName { get; set; }
		public virtual string CustomerEmail { get; set; }

		public virtual int ShippingId { get; set; }
		public virtual int TaxId { get; set; }

		protected IList<OrderDetail> _orderDetails;
		public virtual IEnumerable<OrderDetail> OrderDetails { get { return _orderDetails; } }

		public virtual void AddOrderDetail(OrderDetail detail) {
			detail.Order = this;
			_orderDetails.Add(detail);
		}
	}

	public class OrderMap : ClassMap<Order>
	{
		public OrderMap()
		{
			Table("Orders");

			Id(x => x.Id).GeneratedBy.Identity().Column("OrderID");
			Map(x => x.CustomerId);
			Map(x => x.CustomerName);
			Map(x => x.CustomerEmail);
			Map(x => x.ShippingAddress);
			Map(x => x.ShippingId);
			Map(x => x.TaxId);

			HasMany(x => x.OrderDetails).Access.CamelCaseField(Prefix.Underscore).KeyColumn("OrderId").Cascade.AllDeleteOrphan();
		}
	}
}
