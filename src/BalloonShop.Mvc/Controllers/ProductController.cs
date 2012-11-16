using System.Web.Mvc;
using NHibernate;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISession _session;

        public ProductController(ISession session)
        {
            _session = session;
        }

        public ActionResult Show(int id)
        {
            var balloon = _session.Get<Product>(id);

            ViewBag.Recomendations = _session.QueryOver<Product>().Where(x => x.Id != id).Take(5).List();

            return View(balloon);
        }

    }
}
