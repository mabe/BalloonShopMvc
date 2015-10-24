using System.Web.Mvc;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using BalloonShop.Queries;
using System.Web;

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
			
		public ActionResult Show(string department, string category, int? page = 1)
        {
			var c = _session.QueryOver<Category>().Where(x => x.Name == HttpUtility.UrlDecode(category)).SingleOrDefault();

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.Categories = _session.CategoriesInDepartment(c.Department.Id).List();
            ViewBag.DepartmentId = c.Department.Id;
            ViewBag.CategoryId = c.Id;
			ViewBag.Balloons = _session.BalloonsInCategory(c.Id).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

            return View(c);
        }
    }
}
