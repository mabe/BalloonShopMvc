using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;

namespace BalloonShop.Mvc.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Show(string search, bool allWords, int? page)
        {
            int howManyPages;
            var result = CatalogAccess.Search(search, allWords, page ?? 1, out howManyPages);
            ViewData["search"] = search;
            ViewData["allWords"] = allWords;
            return View(new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, result));
        }

    }
}
