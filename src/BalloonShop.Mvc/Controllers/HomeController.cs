using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;

using System.Linq;
using NHibernate;


namespace BalloonShop.Mvc.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly ISession _session;

        public HomeController(ISession session)
        {
            _session = session;
        }

        public ViewResult Index(int? page = 1)
        {
            var query = _session.QueryOver<Balloon>().Where(x => x.OnCatalogPromotion == true);

            var howManyPages = query.Clone().ToRowCountQuery().RowCount() / BalloonShopConfiguration.ProductsPerPage;

            var balloons = query.Skip(((page ?? 1) - 1) * BalloonShopConfiguration.ProductsPerPage)
                .Take(BalloonShopConfiguration.ProductsPerPage)
                .List();

            return View(new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons));
        }
    }
}
