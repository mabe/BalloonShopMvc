using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using BalloonShop.Queries;

namespace BalloonShop.Mvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ISession _session;

        public CategoryController(ISession session)
        {
            _session = session;
        }

        public ActionResult Navigation(int departmentId, int? selectedCategoryId)
        {
            ViewData["CategoryId"] = selectedCategoryId ?? 0;

            return PartialView(_session.CategoriesInDepartment(departmentId).List());
        }

        public ActionResult Show(int id, int? page = 1)
        {
            ViewBag.Balloons = _session.BalloonsInCategory(id).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

            return View(_session.Get<Category>(id));
        }
    }
}
