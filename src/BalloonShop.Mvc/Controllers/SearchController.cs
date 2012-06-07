using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BalloonShop.Data;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;

namespace BalloonShop.Mvc.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISession _session;

        public SearchController(ISession session)
        {
            _session = session;
        }

        //
        // GET: /Search/

        public ActionResult Show(string search, bool allWords, int? page)
        {
            // transform search string into array of words
            char[] wordSeparators = new char[] { ',', ';', '.', '!', '?', '-', ' ' };
            string[] words = search.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries);

            var query = _session.GetNamedQuery("SearchCatalog")
                .SetParameter("AllWords", allWords);

            for (int i = 0; i < 5; i++) {
                query.SetParameter("Word" + (i + 1), (words.Length > i && words[i].Length > 2) ? (object)words[i] : DBNull.Value);
            }

            var balloons = query.List<Balloon>();

            var howManyPages = balloons.Count() / BalloonShopConfiguration.ProductsPerPage;

            var result = balloons.Skip(((page ?? 1) - 1) * BalloonShopConfiguration.ProductsPerPage)
                .Take(BalloonShopConfiguration.ProductsPerPage)
                .ToList();

            ViewData["search"] = search;
            ViewData["allWords"] = allWords;
            return View(new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, result));
        }

    }
}
