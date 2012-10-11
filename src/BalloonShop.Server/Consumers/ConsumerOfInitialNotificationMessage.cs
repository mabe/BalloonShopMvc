//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Rhino.ServiceBus;
//using BalloonShop.Messages;
//using BalloonShop.Services;
//using NHibernate;
//using BalloonShop.Model;

//namespace BalloonShop.Server.Consumers
//{
//    public class ConsumerOfInitialNotificationMessage : ConsumerOf<InitialNotificationMessage>
//    {
//        private readonly IServiceBus _bus;
//        private readonly IEmailService _emailService;
//        private readonly ISession _session;

//        public ConsumerOfInitialNotificationMessage(IServiceBus bus, ISession session, IEmailService emailService)
//        {
//            _bus = bus;
//            _session = session;
//            _emailService = emailService;
//        }

//        public void Consume(InitialNotificationMessage message)
//        {
//            var order = _session.Get<Order>(message.OrderId);

//            _session.Save(new Audit(order.Id, 20000, "InitialNotification started."));

//            var subject = "BalloonShop order received.";

//            var sb = new StringBuilder();
//            sb.Append("Thank you for your order! The products you have "
//              + "ordered are as follows:\n\n");
//            sb.Append(OrderAsString(order));
//            sb.Append("\n\nYour order will be shipped to:\n\n");
//            sb.Append(CustomerAddressAsString(order));
//            sb.Append("\n\nOrder reference number:\n\n");
//            sb.Append(order.Id.ToString());
//            sb.Append(
//              "\n\nYou will receive a confirmation e-mail when this "
//              + "order has been dispatched. Thank you for shopping "
//              + "at BalloonShop!");

//            _emailService.Send("", order.CustomerEmail, subject, sb.ToString());

//            _session.Save(new Audit(order.Id, 20002, "Notification e-mail sent to customer."));

//            order.Status = 1;

//            _session.Save(new Audit(order.Id, 20001, "InitialNotification finished."));
//        }

//        private string CustomerAddressAsString(Order order)
//        {
//            var account = _session.Get<Account>(order.CustomerId);

//            var sb = new StringBuilder();
//            sb.AppendLine(account.Email);
//            sb.AppendLine(account.Details.Address1);
//            sb.AppendLine(account.Details.Address2 ?? "");
//            sb.AppendLine(account.Details.City);
//            sb.AppendLine(account.Details.Region);
//            sb.AppendLine(account.Details.PostalCode);
//            sb.AppendLine(account.Details.Country);

//            return sb.ToString();
//        }

//        private string OrderAsString(Order order)
//        {
//            var shipping = _session.Get<Shipping>(order.ShippingId);
//            var tax = _session.Get<Tax>(order.TaxId);

//            // calculate total cost and set data
//            var sb = new StringBuilder();
//            var TotalCost = 0.0m;
//            foreach (var item in order.OrderDetails)
//            {
//                sb.AppendLine(ItemAsString(item));
//                TotalCost += item.Subtotal;
//            }
//            // Add shipping cost
//            if (shipping != null)
//            {
//                sb.AppendLine("Shipping: " + shipping.Name);
//                TotalCost += shipping.Cost;
//            }
//            // Add tax
//            if (tax != null)
//            {
//                var taxAmount = Math.Round(TotalCost * tax.Percentage, MidpointRounding.AwayFromZero) / 100.0m;
//                sb.AppendLine("Tax: " + tax.Name + ", $" + taxAmount.ToString());
//                TotalCost += taxAmount;
//            }
//            sb.AppendLine();
//            sb.Append("Total order cost: $");
//            sb.Append(TotalCost.ToString());

//            return sb.ToString();
//        }

//        private string ItemAsString(OrderDetail item)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(item.Quantity.ToString());
//            sb.Append(" ");
//            sb.Append(item.ProductName);
//            sb.Append(", $");
//            sb.Append(item.UnitCost.ToString());
//            sb.Append(" each, total cost $");
//            sb.Append(item.Subtotal.ToString());

//            return sb.ToString();
//        }
//    }
//}
