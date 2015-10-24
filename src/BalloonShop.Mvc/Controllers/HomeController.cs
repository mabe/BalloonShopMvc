using System.Web.Mvc;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using System.Linq;
using NHibernate;
using Rhino.ServiceBus;
using BalloonShop.Messages;
using BalloonShop.Queries;


namespace BalloonShop.Mvc.Controllers
{
    [HandleError]
	[ShoppingCartFilter]
	[CatalogFilter]
	[AccountFilter]
    public class HomeController : Controller
    {
        private readonly ISession _session;

		public HomeController(ISession session)
        {
			_session = session;
        }

        public ViewResult Index(int? page = 1)
        {
			//.Where(x => x.OnCatalogPromotion == true);
			var products = _session.QueryOver<Product>().PagedList(BalloonShopConfiguration.ProductsPerPage, page);
			return View(products);
        }
    }
}
