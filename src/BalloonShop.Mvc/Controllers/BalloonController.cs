using System.Web.Mvc;
using BalloonShop.Data;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Controllers
{
    public class BalloonController : Controller
    {
        public ActionResult Show(int id)
        {
            var balloon = CatalogAccess.GetProductDetails(id);

            
            return View(balloon);
        }

    }
}
