using System.Web.Mvc;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using BalloonShop.Queries;

namespace BalloonShop.Mvc.Controllers
{
	[ShoppingCartFilter]
    public class CategoryController : Controller
    {
        private readonly ISession _session;

        public CategoryController(ISession session)
        {
            _session = session;
        }

        public ActionResult Show(int id, int? page = 1)
        {
            var category = _session.Get<Category>(id);

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.Categories = _session.CategoriesInDepartment(category.Department.Id).List();
            ViewBag.DepartmentId = category.Department.Id;
            ViewBag.CategoryId = category.Id;
            ViewBag.Balloons = _session.BalloonsInCategory(id).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

            return View(category);
        }
    }
}
