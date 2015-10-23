using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BalloonShop.Infrastructure;
using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;

namespace BalloonShop.Mvc.Controllers
{
	using Dapper;
	using NHibernate.Engine;
	using NHibernate.Hql.Ast.ANTLR;
	using NHibernate.Impl;
	using NHibernate.Loader;
	using NHibernate.Loader.Criteria;
	using NHibernate.Persister.Entity;

	[CatalogFilter]
    public class SearchController : Controller
    {
		public class SearchRank {
			public int Id {
				get;
				set;
			}
			public decimal Rank {
				get;
				set;
			}
		}

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
			/*
            var query = _session.GetNamedQuery("SearchCatalog")
                .SetParameter("AllWords", allWords);

            for (int i = 0; i < 5; i++) {
                query.SetParameter("Word" + (i + 1), (words.Length > i && words[i].Length > 2) ? (object)words[i] : DBNull.Value);
            }

			*/

			var parameter = "'" + search + "'"; // string.Join (" || ", words.Select (x => "'" + x + "'").ToArray ());
			var query = string.Format(@"select ProductId Id, ts_rank(to_tsvector('english', coalesce(product.description::text, '')), to_tsquery('english', ''' ' || {0} || ' ''')) Rank from product order by Rank desc", parameter);

			var rank = _session.Connection.Query<SearchRank>(query).Where(x => x.Rank > 0).Select(x => x.Id).ToArray();
			var balloons = _session.QueryOver<Product> ().Where (Restrictions.In ("Id", rank)).List ();

            var hits = balloons.Count();
            var howManyPages = hits / BalloonShopConfiguration.ProductsPerPage;

            var result = balloons.Skip(((page ?? 1) - 1) * BalloonShopConfiguration.ProductsPerPage)
                .Take(BalloonShopConfiguration.ProductsPerPage)
                .ToList();

            ViewData["search"] = search;
            ViewData["allWords"] = allWords;
            ViewData["hits"] = hits;

            return View(new PagedList<Product>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, result));
        }

		public string ToSql(ICriteria criteria)
		{
			var c = (CriteriaImpl) criteria;
			var s = (SessionImpl)c.Session;
			var factory = (ISessionFactoryImplementor)s.SessionFactory;
			String[] implementors = factory.GetImplementors(c.EntityOrClassName);
			var loader = new CriteriaLoader(
				(IOuterJoinLoadable)factory.GetEntityPersister(implementors[0]),
				factory, 
				c, 
				implementors[0], 
				s.EnabledFilters);

			return ((OuterJoinLoader)loader).SqlString.ToString();
		}

    }
}
