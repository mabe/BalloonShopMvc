﻿using BalloonShop.Messages;
using BalloonShop.Model;
using BalloonShop.Services;
using NHibernate;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalloonShop.Server.Sagas
{
    [Serializable]
    public class OrderPipelineState
    {
        public int OrderId { get; set; }
    }

    public class OrderPipelineSaga :
                    ISaga<OrderPipelineState>,
                    InitiatedBy<InitialNotificationMessage>,
                    Orchestrates<PSCheckFundsMessage>,
                    Orchestrates<PSCheckStockMessage>,
                    Orchestrates<PSStockOKMessage>,
                    Orchestrates<PSTakePaymentMessage>,
                    Orchestrates<PSShipGoodsMessage>,
                    Orchestrates<PSShipOKMessage>,
                    Orchestrates<PSFinalNotificationMessage>
    {
        private readonly IServiceBus _bus;
        private readonly IEmailService _emailService;
        private readonly ISession _session;

        public OrderPipelineState State { get; set; }

        public Guid Id { get; set; }

        public bool IsCompleted { get; set; }

        public OrderPipelineSaga(IServiceBus bus, ISession session, IEmailService emailService)
        {
            _bus = bus;
            _session = session;
            _emailService = emailService;
            State = new OrderPipelineState();
        }

        public void Consume(InitialNotificationMessage message)
        {
            State.OrderId = message.OrderId;

            MakeAudit(20000, "InitialNotification started.");

            var order = _session.Get<Order>(State.OrderId);
            var account = _session.Get<Account>(order.CustomerId);

            var subject = "BalloonShop order received.";

            var sb = new StringBuilder();
            sb.Append("Thank you for your order! The products you have "
              + "ordered are as follows:\n\n");
            sb.Append(order.AsString());
            sb.Append("\n\nYour order will be shipped to:\n\n");
            sb.Append(account.Address());
            sb.Append("\n\nOrder reference number:\n\n");
            sb.Append(order.Id.ToString());
            sb.Append(
              "\n\nYou will receive a confirmation e-mail when this "
              + "order has been dispatched. Thank you for shopping "
              + "at BalloonShop!");

            _emailService.Send("", order.CustomerEmail, subject, sb.ToString());

            MakeAudit(20002, "Notification e-mail sent to customer.");

            order.Status = 1;

            MakeAudit(20001, "InitialNotification finished.");

            _bus.SendToSelf(new PSCheckFundsMessage() { CorrelationId = message.CorrelationId });
        }

        public void Consume(PSCheckFundsMessage message)
        {
            MakeAudit(20100, "PSCheckFunds started.");

            try
            {
                var order = _session.Get<Order>(State.OrderId);

                // check customer funds via DataCash gateway
                // configure DataCash XML request
                //    var request = new DataCashRequest() { 
                //                          Authentication.Client = BalloonShopConfiguration.DataCashClient, 
                //                          Authentication.Password = BalloonShopConfiguration.DataCashPassword,
                //                          Transaction.TxnDetails.MerchantReference = order.Id.ToString().PadLeft(6, '0').PadLeft(7, '5'),
                //                          Transaction.TxnDetails.Amount.Amount = order.TotalCost.ToString(),
                //                          Transaction.TxnDetails.Amount.Currency = "GBP",
                //                          Transaction.CardTxn.Method = "pre",
                //                          Transaction.CardTxn.Card.CardNumber = order.CreditCard.CardNumber,
                //                          Transaction.CardTxn.Card.ExpiryDate = order.CreditCard.ExpiryDate,
                //                          Transaction.CardTxn.Card.StartDate = (order.CreditCard.IssueDate != "") ? order.CreditCard.IssueDate : null,
                //                          Transaction.CardTxn.Card.IssueNumber = (0rder.CreditCard.IssueNumber != "") ? order.CreditCard.IssueNumber : null
                //                      };

                var request = new { Xml = "" };

                // get DataCash response
                //var response = request.GetResponse(BalloonShopConfiguration.DataCashUrl);

                var response = new { Status = "1", Xml = "", MerchantReference = "MerchantReference", DatacashReference = "DatacashReference" };

                if (response.Status == "1")
                {
                    order.SetAuthCodeAndReference(response.MerchantReference, response.DatacashReference);

                    MakeAudit(20102, "Funds available for purchase.");

                    order.Status = 2;
                }
                else
                {

                    MakeAudit(20103, "Funds not available for purchase.");

                    var to = ""; // BalloonShopConfiguration.ErrorLogEmail;
                    var from = ""; // BalloonShopConfiguration.OrderProcessorEmail;
                    var body = "Message: " + "XML data exchanged:\n" + request.Xml + "\n\n" + response.Xml + "\nSource: " + 1.ToString() + "\nOrder ID: " + order.Id.ToString();

                    _emailService.Send(from, to, "Credit card declined.", body);
                }
            }
            catch
            {
                // fund checking failure
                //throw new OrderProcessorException("Error occured while checking funds.", 1);

                throw new Exception();
            }

            MakeAudit(20101, "PSCheckFunds finished.");

            _bus.SendToSelf(new PSCheckStockMessage() { CorrelationId = message.CorrelationId });
        }

        public void Consume(PSCheckStockMessage message)
        {
            MakeAudit(20200, "PSCheckStock started.");

            try
            {
                var order = _session.Get<Order>(State.OrderId);

                // construct message body
                var sb = new StringBuilder();
                sb.Append("The following goods have been ordered:\n\n");
                sb.Append(order.AsString());
                sb.Append("\n\nPlease check availability and confirm via ");
                sb.Append("http://balloonshop.apress.com/OrdersAdmin.aspx");
                sb.Append("\n\nOrder reference number:\n\n");
                sb.Append(order.Id.ToString());

                var to = ""; // BalloonShopConfiguration.SupplierEmail;
                var from = ""; // BalloonShopConfiguration.OrderProcessorEmail;

                // send mail to supplier
                _emailService.Send(from, to, "BalloonShop stock check.", sb.ToString());

                MakeAudit(20202, "Notification e-mail sent to supplier.");

                // update order status
                order.Status = 3;
            }
            catch
            {
                // mail sending failure
                //throw new OrderProcessorException("Unable to send e-mail to supplier.", 2);

                throw new Exception();
            }

            MakeAudit(20201, "PSCheckStock finished.");
        }

        public void Consume(PSStockOKMessage message)
        {
            // audit
            MakeAudit(20300, "PSStockOK started.");

            var order = _session.Get<Order>(State.OrderId);

            MakeAudit(20302, "Stock confirmed by supplier.");

            order.Status = 4;

            MakeAudit(20301, "PSStockOK finished.");

            _bus.Send(new PSTakePaymentMessage() { CorrelationId = message.CorrelationId });
        }

        public void Consume(PSTakePaymentMessage message)
        {
            MakeAudit(20400, "PSTakePayment started.");

            try
            {
                var order = _session.Get<Order>(State.OrderId);

                //var request = new DataCashRequest() { 
                //                      Authentication.Client = BalloonShopConfiguration.DataCashClient, 
                //                      Authentication.Password = BalloonShopConfiguration.DataCashPassword,
                //                      Transaction.HistoricTxn.Method = "fulfill",
                //                      Transaction.HistoricTxn.AuthCode = order.AuthCode,
                //                      Transaction.HistoricTxn.Reference = order.Reference
                //                  };
                var request = new { Xml = "" };

                //var response = request.GetResponse(BalloonShopConfiguration.DataCashUrl);
                var response = new { Status = "1", Xml = "" };

                if (response.Status == "1")
                {
                    MakeAudit(20402, "Funds deducted from customer credit card account.");

                    order.Status = 5;
                }
                else
                {
                    MakeAudit(20403, "Error taking funds from customer credit card account.");

                    var to = ""; // BalloonShopConfiguration.ErrorLogEmail;
                    var from = ""; // BalloonShopConfiguration.OrderProcessorEmail;
                    var body = "Message: " + "XML data exchanged:\n" + request.Xml + "\n\n" + response.Xml + "\nSource: " + 1.ToString() + "\nOrder ID: " + order.Id.ToString();

                    _emailService.Send(from, to, "Credit card fulfillment declined.", body);
                }
            }
            catch
            {
                // fund checking failure
                //throw new OrderProcessorException("Error occured while taking payment.", 4);
                throw new Exception();
            }

            MakeAudit(20401, "PSTakePayment finished.");

            _bus.Send(new PSShipGoodsMessage() { CorrelationId = message.CorrelationId });
        }

        public void Consume(PSShipGoodsMessage message)
        {
            MakeAudit(20500, "PSShipGoods started.");

            try
            {
                var order = _session.Get<Order>(State.OrderId);
                var account = _session.Get<Account>(order.CustomerId);

                var sb = new StringBuilder();
                sb.Append(
                  "Payment has been received for the following goods:\n\n");
                sb.Append(order.AsString());
                sb.Append("\n\nPlease ship to:\n\n");
                sb.Append(account.Address());
                sb.Append("\n\nWhen goods have been shipped, please confirm via ");
                sb.Append("http://balloonshop.apress.com/OrdersAdmin.aspx");
                sb.Append("\n\nOrder reference number:\n\n");
                sb.Append(order.Id.ToString());

                var to = ""; // BalloonShopConfiguration.SupplierEmail;
                var from = ""; // BalloonShopConfiguration.OrderProcessorEmail;

                _emailService.Send(from, to, "BalloonShop ship goods.", sb.ToString());

                MakeAudit(20502, "Ship goods e-mail sent to supplier.");

                order.Status = 6;
            }
            catch
            {
                // mail sending failure
                //throw new OrderProcessorException("Unable to send e-mail to supplier.", 5);
                throw new Exception();
            }

            MakeAudit(20501, "PSShipGoods finished.");
        }

        public void Consume(PSShipOKMessage message)
        {
            var order = _session.Get<Order>(State.OrderId);

            order.SetDateShipped();
            
            MakeAudit(20602, "Order dispatched by supplier.");
            
            order.Status = 7;
            
            MakeAudit(20601, "PSShipOK finished.");

            _bus.Send(new PSFinalNotificationMessage() { CorrelationId = message.CorrelationId });
        }

        public void Consume(PSFinalNotificationMessage message)
        {
            MakeAudit(20700, "PSFinalNotification started.");

            try
            {
                var order = _session.Get<Order>(State.OrderId);
                var account = _session.Get<Account>(order.CustomerId);

                var sb = new StringBuilder();
                sb.Append("Your order has now been dispatched! The following " + "products have been shipped:\n\n");
                sb.Append(order.AsString());
                sb.Append("\n\nYour order has been shipped to:\n\n");
                sb.Append(account.Address());
                sb.Append("\n\nOrder reference number:\n\n");
                sb.Append(order.Id.ToString());
                sb.Append("\n\nThank you for shopping at BalloonShop!");
                
                _emailService.Send("", order.CustomerEmail, "BalloonShop order dispatched.", sb.ToString());

                MakeAudit(20702, "Dispatch e-mail sent to customer.");
   
                order.Status = 8;
            }
            catch
            {
                throw new Exception("Unable to send e-mail to customer.");
            }
            
            MakeAudit(20701, "PSFinalNotification finished.");
        }

        private void MakeAudit(int number, string message)
        {
            _session.Save(new Audit(State.OrderId, number, message));
        }
    }
}
