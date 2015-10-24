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
    public class DepartmentController : Controller
    {
        private readonly ISession _session;

        public DepartmentController(ISession session)
        {
            _session = session;
        }
			
        public ActionResult Show(string department, int? page)
        {
			var d = _session.QueryOver<Department>().Where(x => x.Name == HttpUtility.UrlDecode(department)).SingleOrDefault();

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.Categories = _session.CategoriesInDepartment(d.Id).List();
			ViewBag.DepartmentId = d.Id;
			ViewBag.CategoryId = 0;
            ViewBag.PromotedBalloons = _session.BalloonsInDepartment(d).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

            return View(d);
        }
    }
}
