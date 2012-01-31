using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ISession _session;

        public CartController(ISession session)
        {
            _session = session;
        }

        [HttpPost]
        public ActionResult Add(string customerCartId, int balloonId)
        {
            var balloon = _session.Load<Balloon>(balloonId);

            var item = _session.Get<ShoppingCart>(new ShoppingCart { Balloon = balloon, CartId = customerCartId });

            if (item == null) {
                item = new ShoppingCart() { Balloon = balloon, CartId = customerCartId, DateAdded = DateTime.Now };
                _session.Save(item);
            }

            item.Quantity += 1;

            return RedirectToAction("Show", "Balloon", new { id = balloonId });
        }

        [HttpPost]
        public ActionResult Remove(string customerCartId, int remove) {
            _session.Delete(_session.Load<ShoppingCart>(new ShoppingCart() { CartId = customerCartId, Balloon = _session.Load<Balloon>(remove) }));

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult Update(string customerCartId, IDictionary<int, int> items) {
            var cart = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();

            foreach (var item in items)
            {
                cart.Single(x => x.Balloon.Id == item.Key).Quantity = item.Value;
            }

            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Summary(string customerCartId) {
            var model = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();

            ViewBag.Total = model.Sum(x => x.Balloon.Price * x.Quantity);

            return View(model);
        }

        public ActionResult Index(string customerCartId) {
            var model = _session.QueryOver<ShoppingCart>().Where(x => x.CartId == customerCartId).List();

            ViewBag.Total = model.Sum(x => x.Balloon.Price * x.Quantity);

            return View(model);
        }

    }
}
