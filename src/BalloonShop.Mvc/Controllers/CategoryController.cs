using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;

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
            return PartialView(_session.QueryOver<Category>().Where(x => x.Department.Id == departmentId).List());
        }

        public ActionResult Show(int id, int? page = 1)
        {
            var category = _session.Get<Category>(id);

            Category c = null;

            var query = _session.QueryOver<Balloon>().JoinAlias(x => x.Categories, () => c).Where(Restrictions.On<Category>(x => c.Id).IsIn(new []{ id }));

            var howManyPages = query.Clone().RowCount() / BalloonShopConfiguration.ProductsPerPage;

            var balloons = query
                .Skip(((page ?? 1) - 1) * BalloonShopConfiguration.ProductsPerPage)
                .Take(BalloonShopConfiguration.ProductsPerPage)
                .List();

            ViewBag.Balloons = new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons);
            return View(category);
        }
    }
}
