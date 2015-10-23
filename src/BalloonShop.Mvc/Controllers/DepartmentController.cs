using System.Web.Mvc;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using BalloonShop.Queries;

namespace BalloonShop.Mvc.Controllers
{
	[ShoppingCartFilter]
    public class DepartmentController : Controller
    {
        private readonly ISession _session;

        public DepartmentController(ISession session)
        {
            _session = session;
        }

        public ActionResult Show(int id, int? page)
        {
            var department = _session.Get<Department>(id);

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.Categories = _session.CategoriesInDepartment(id).List();
            ViewBag.DepartmentId = id;
			ViewBag.CategoryId = 0;
            ViewBag.PromotedBalloons = _session.BalloonsInDepartment(department).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

            return View(department);
        }
    }
}
