using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;

using System.Linq;
using BalloonShop.Data.Linq2Sql;

namespace BalloonShop.Mvc.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ViewResult Index(int? page)
        {
            //int howManyPages;
            //var balloons = CatalogAccess.GetProductsOnCatalogPromotion(page ?? 1,
            //    out howManyPages);

            var context = new BalloonShopDataDataContext();

            var howManyPages = context.Products.Where(x => x.OnCatalogPromotion).Count() / BalloonShopConfiguration.ProductsPerPage;
            var balloons = context.Products
                .Where(x => x.OnCatalogPromotion)
                .Skip(((page ?? 1) - 1) * BalloonShopConfiguration.ProductsPerPage)
                .Take(BalloonShopConfiguration.ProductsPerPage)
                .ToList().Select(x => new Balloon
                {
                    Description = x.Description,
                    Image = x.Image2FileName,
                    Name = x.Name,
                    OnCatalogPromotion = x.OnCatalogPromotion,
                    OnDepartmentPromotion = x.OnDepartmentPromotion,
                    Price = x.Price,
                    Thumb = x.Image1FileName
                });

            return View(new PagedList<Balloon>(page ?? 1,
                BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons));

            //return View(new PagedList<Product>(page ?? 1,
            //    BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons));
        }
    }
}
