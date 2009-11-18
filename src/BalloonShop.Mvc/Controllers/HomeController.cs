using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ViewResult Index(int? page)
        {
            int howManyPages;
            var balloons = CatalogAccess.GetProductsOnCatalogPromotion(page ?? 1,
                out howManyPages); 

            return View(new PagedList<Balloon>(page ?? 1, 
                BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons));
        }
    }
}
