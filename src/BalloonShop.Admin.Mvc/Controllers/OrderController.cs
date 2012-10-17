using BalloonShop.Messages;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order = BalloonShop.Model.Order;

namespace BalloonShop.Admin.Mvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ISession _session;
        private readonly IOnewayBus _bus;

        public OrderController(ISession session, IOnewayBus bus)
        {
            _session = session;
            _bus = bus;
        }

        public ActionResult Index(Guid? customer, int? orderid, int? recentcount, DateTime? startdate, DateTime? enddate, string filter)
        {
            ViewBag.Customers = _session.QueryOver<Account>().List();
            ViewBag.CustomerId = customer;

            var query = _session.QueryOver<Order>();

            if (customer.HasValue) {
                query.Where(x => x.CustomerId == customer.Value);
            }

            if (orderid.HasValue) {
                query.Where(x => x.Id == orderid.Value);
            }

            if (recentcount.HasValue) {
                query.OrderBy(x => x.DateCreated).Desc.Take(recentcount.Value);
            }

            if (startdate.HasValue && enddate.HasValue) {
                query.Where(Restrictions.Between("DateCreated", startdate.Value, enddate.Value));
            }

            if (filter == "awatingstockcheck") { 
                query.Where(x => x.Status == 3);
            }

            if (filter == "awatingshipment") { 
                query.Where(x => x.Status == 6);
            }
        

            return View(query.List());
        }

        public ActionResult Edit(int id) {
            var order = _session.Get<Order>(id);

            ViewBag.Customer = _session.Get<Account>(order.CustomerId);
            ViewBag.Audits = _session.QueryOver<Audit>().Where(x => x.OrderId == id).List();
            ViewBag.OrderProcessFinished = order.Status == 8 || order.Status == 9;

            return View(order);
        }

        private static IDictionary<int, Func<Guid, ISagaMessage>> messages = new Dictionary<int, Func<Guid, ISagaMessage>> { 
            //{ 0, id => new InitialNotificationMessage() { CorrelationId = id } },
            { 1, id => new PSCheckFundsMessage() { CorrelationId = id } },
            { 2, id => new PSCheckStockMessage() { CorrelationId = id } },
            { 3, id => new PSStockOKMessage() { CorrelationId = id } },
            { 4, id => new PSTakePaymentMessage() { CorrelationId = id } },
            { 5, id => new PSShipGoodsMessage() { CorrelationId = id } },
            { 6, id => new PSShipOKMessage() { CorrelationId = id } },
            { 7, id => new PSFinalNotificationMessage() { CorrelationId = id } },
        };

        [HttpPost]
        public ActionResult Process(int id) {
            var order = _session.Get<Order>(id);

            var status = order.Status;

            if (messages.ContainsKey(status)) {
                _bus.Send(messages[status](order.SagaCorrelationId));
            }

            return RedirectToAction("Edit", "Order", new { id = id });
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            _session.Get<Order>(id).Status = 9;

            return RedirectToAction("Edit", "Order", new { id = id });
        }

        public ActionResult ShoppingCarts() {
            return View();
        }

        [HttpPost]
        public ActionResult ShoppingCarts(string action, int days) {
            var query = _session.QueryOver<ShoppingCart>();

            if (days > 0) {
                query.Where(Restrictions.On<ShoppingCart>(x => x.DateAdded).IsBetween(DateTime.Today.AddDays(days * -1)).And(DateTime.Today));
            }

            if (action == "count") {
                ViewBag.Count = query.Select(Projections.CountDistinct("CartId")).RowCount();
            }

            if (action == "delete") {
                _session.CreateQuery("delete from ShoppingCart where cartid IN :ids")
                    .SetParameterList("ids", query.Select(Projections.Distinct(Projections.Property("CartId"))).List<string>())
                    .ExecuteUpdate();
            }

            return View();
        }
    }
}
