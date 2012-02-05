using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
	public class Order
	{
		public virtual int Id { get; set; }
		public virtual string CustomerName { get; set; }
		public virtual string CustomerEmail { get; set; }
		public virtual string ShippingAddress { get; set; }

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
			Map(x => x.CustomerName);
			Map(x => x.CustomerEmail);
			Map(x => x.ShippingAddress);

			HasMany(x => x.OrderDetails).Access.CamelCaseField(Prefix.Underscore);
		}
	}
}
