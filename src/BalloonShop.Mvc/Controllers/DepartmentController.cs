using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;

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
            ViewData["DepartmentId"] = DepartmentId;
            var departments = _session.QueryOver<Department>().List();

            return View(departments);
        }

        public ActionResult Show(int id, int? page)
        {
            int howManyPages;
            var department = _session.Get<Department>(id);
            var balloons = CatalogAccess.GetProductsOnDepartmentPromotion(department.Id, page ?? 1, out howManyPages);

            ViewBag.PromotedBalloons = new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons);

            return View(department);
        }
    }
}
