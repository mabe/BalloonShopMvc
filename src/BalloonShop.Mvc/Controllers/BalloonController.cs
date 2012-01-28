using System.Web.Mvc;
using BalloonShop.Data;
using NHibernate;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Controllers
{
    public class BalloonController : Controller
    {
        private readonly ISession _session;

        public BalloonController(ISession session)
        {
            _session = session;
        }

        public ActionResult Show(int id)
        {
            var balloon = _session.Get<Balloon>(id);

            
            return View(balloon);
        }

    }
}
