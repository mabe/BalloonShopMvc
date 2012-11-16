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
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IOnewayBus _bus;

        public HomeController(ISession session, IOnewayBus bus)
        {
            _session = session;
            _bus = bus;
        }

        public ViewResult Index(int? page = 1)
        {
            return View(_session.QueryOver<Product>().Where(x => x.OnCatalogPromotion == true).PagedList(BalloonShopConfiguration.ProductsPerPage, page));
        }
    }
}
