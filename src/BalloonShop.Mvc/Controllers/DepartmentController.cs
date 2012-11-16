using System.Web.Mvc;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using BalloonShop.Queries;

namespace BalloonShop.Mvc.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ISession _session;

        public DepartmentController(ISession session)
        {
            _session = session;
        }

        public ActionResult Navigation(int? DepartmentId = 0)
        {
            ViewBag.DepartmentId = DepartmentId;

            return PartialView(_session.QueryOver<Department>().List());
        }

        public ActionResult Show(int id, int? page)
        {
            var department = _session.Get<Department>(id);

            ViewBag.DepartmentId = id;
            ViewBag.PromotedBalloons = _session.BalloonsInDepartment(department).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

            return View(department);
        }
    }
}
