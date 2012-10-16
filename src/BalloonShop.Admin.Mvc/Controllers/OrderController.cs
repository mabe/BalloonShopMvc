using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
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

        public OrderController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            return View(_session.QueryOver<Order>().List());
        }

        public ActionResult Edit(int id) {
            ViewBag.Audits = _session.QueryOver<Audit>().Where(x => x.OrderId == id).List();

            return View(_session.Get<Order>(id));
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
