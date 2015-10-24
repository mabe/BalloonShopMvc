using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace BalloonShop.Model
{
    public class Order
    {
        protected Order()
        {
            _orderDetails = new List<OrderDetail>();
        }

        public Order(Guid customer, string name, string email, IEnumerable<OrderDetail> details, Tax tax, Shipping shipping)
            : this()
        {
            CustomerId = customer;
            CustomerName = name;
            CustomerEmail = email;
            SagaCorrelationId = Guid.NewGuid();
            DateCreated = DateTime.Now;
            foreach (var item in details) {
                AddOrderDetail(item);
            }
            SetShipping(shipping);
            SetTax(tax);
        }

        public virtual int Id { get; set; }

        public virtual string ShippingAddress { get; set; }

        public virtual Guid CustomerId { get; protected set; }
        public virtual string CustomerName { get; protected set; }
        public virtual string CustomerEmail { get; protected set; }

        public virtual Shipping Shipping { get; protected set; }
        public virtual Tax Tax { get; protected set; }
        public virtual int Status { get; set; }

        public virtual DateTime DateCreated { get; protected set; }
        public virtual DateTime? DateShipped { get; protected set; }

        protected IList<OrderDetail> _orderDetails;
        public virtual IEnumerable<OrderDetail> OrderDetails { get { return _orderDetails; } }

        public virtual void AddOrderDetail(OrderDetail detail)
        {
            detail.Order = this;
            _orderDetails.Add(detail);

            TotalCost += detail.UnitCost + detail.Quantity;
        }

        protected virtual void SetShipping(Shipping shipping)
        {
            Shipping = shipping;
            TotalCost += shipping.Cost;
        }

        protected virtual void SetTax(Tax tax)
        {
            Tax = tax;
            TotalCost += Math.Round(TotalCost * Tax.Percentage, MidpointRounding.AwayFromZero) / 100.0m;
        }

        public virtual void SetAuthCodeAndReference(string authCode, string reference)
        {
            AuthCode = authCode;
            Reference = reference;
        }

        public virtual string AuthCode { get; set; }

        public virtual string Reference { get; set; }

        public virtual Guid SagaCorrelationId { get; set; }

        public virtual decimal TotalCost { get; set; }

        public virtual string Comments { get; set; }

        public virtual string StatusText()
        {
            return OrderStatuses[Status];
        }

        private readonly string[] OrderStatuses = {
              "Order placed, notifying customer",  // 0
              "Awaiting confirmation of funds",    // 1
              "Notifying supplier—stock check",    // 2
              "Awaiting stock confirmation",       // 3
              "Awaiting credit card payment",      // 4
              "Notifying supplier—shipping",       // 5
              "Awaiting shipment confirmation",    // 6
              "Sending final notification",        // 7
              "Order completed",                   // 8
              "Order cancelled"                    // 9
        };

        public virtual void SetDateShipped()
        {
            DateShipped = DateTime.Now;
        }

        public virtual string AsString()
        {
            var shipping = Shipping;
            var tax = Tax;

            // calculate total cost and set data
            var sb = new StringBuilder();
            var TotalCost = 0.0m;
            foreach (var item in OrderDetails)
            {
                sb.AppendLine(ItemAsString(item));
                TotalCost += item.Subtotal;
            }
            // Add shipping cost
            if (shipping != null)
            {
                sb.AppendLine("Shipping: " + shipping.Name);
                TotalCost += shipping.Cost;
            }
            // Add tax
            if (tax != null)
            {
                var taxAmount = Math.Round(TotalCost * tax.Percentage, MidpointRounding.AwayFromZero) / 100.0m;
                sb.AppendLine("Tax: " + tax.Name + ", $" + taxAmount.ToString());
                TotalCost += taxAmount;
            }
            sb.AppendLine();
            sb.Append("Total order cost: $");
            sb.Append(TotalCost.ToString());

            return sb.ToString();
        }

        private string ItemAsString(OrderDetail item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(item.Quantity.ToString());
            sb.Append(" ");
            sb.Append(item.ProductName);
            sb.Append(", $");
            sb.Append(item.UnitCost.ToString());
            sb.Append(" each, total cost $");
            sb.Append(item.Subtotal.ToString());

            return sb.ToString();
        }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("orders");

            Id(x => x.Id).GeneratedBy.Identity().Column("OrderID");
            Map(x => x.CustomerId);
            Map(x => x.CustomerName);
            Map(x => x.CustomerEmail);
            Map(x => x.ShippingAddress);
            Map(x => x.Status);
            Map(x => x.AuthCode);
            Map(x => x.Reference);
            Map(x => x.SagaCorrelationId);
            Map(x => x.DateCreated);
            Map(x => x.DateShipped);
            Map(x => x.TotalCost);
            Map(x => x.Comments);

            References(x => x.Shipping, "ShippingId");
            References(x => x.Tax, "TaxId");

            HasMany(x => x.OrderDetails).Access.CamelCaseField(Prefix.Underscore).KeyColumn("OrderId").Cascade.AllDeleteOrphan();
        }
    }
}
