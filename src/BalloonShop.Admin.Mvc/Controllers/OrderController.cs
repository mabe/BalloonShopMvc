using BalloonShop.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    }
}
