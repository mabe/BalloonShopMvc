using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;

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
            var department = _session.Get<Department>(id);

            Balloon b = null;
            var query = _session.QueryOver<Balloon>().WithSubquery.WhereProperty(x => x.Id).In(QueryOver.Of<Balloon>(() => b).Where(x => x.OnDepartmentPromotion == true).JoinQueryOver(x => x.Categories).Where(x => x.Department == department).Select(Projections.Distinct(Projections.Property(() => b.Id))));
            
            //int howManyPages;
            //var balloons = CatalogAccess.GetProductsOnDepartmentPromotion(department.Id, page ?? 1, out howManyPages);
            var howManyPages = query.Clone().RowCount() / BalloonShopConfiguration.ProductsPerPage;

            var balloons = query
                .Skip(((page ?? 1) - 1) * BalloonShopConfiguration.ProductsPerPage)
                .Take(BalloonShopConfiguration.ProductsPerPage)
                .List();

            ViewBag.PromotedBalloons = new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons);

            return View(department);
        }
    }
}
