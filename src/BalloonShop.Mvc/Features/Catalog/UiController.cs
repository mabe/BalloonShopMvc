using Dapper;
using System;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using BalloonShop.Model;
using BalloonShop.Queries;
using System.Collections.Generic;
using NHibernate.Criterion;
using BalloonShop.Infrastructure;

namespace BalloonShop.Mvc.Features.Catalog
{
	[ShoppingCartFilter]
	[AccountFilter]
	public class UiController : Controller
	{
		private readonly ISession _session;

		public UiController(ISession session)
		{
			_session = session;
		}

		[CatalogFilter]
		public ViewResult Index(int? page = 1)
		{
			//.Where(x => x.OnCatalogPromotion == true);
			var products = _session.QueryOver<Product>().PagedList(BalloonShopConfiguration.ProductsPerPage, page);
			return View(products);
		}

		public ActionResult Department(string department, int? page)
		{
			var d = _session.QueryOver<Department>().Where(x => x.Name == department).SingleOrDefault();

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.Categories = _session.CategoriesInDepartment(d.Id).List();
			ViewBag.DepartmentId = d.Id;
			ViewBag.CategoryId = 0;
			ViewBag.PromotedBalloons = _session.BalloonsInDepartment(d).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

			return View(d);
		}

		public ActionResult Category(string department, string category, int? page = 1)
		{
			var c = _session.QueryOver<Category>().Where(x => x.Name == category).SingleOrDefault();

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.Categories = _session.CategoriesInDepartment(c.Department.Id).List();
			ViewBag.DepartmentId = c.Department.Id;
			ViewBag.CategoryId = c.Id;
			ViewBag.Balloons = _session.BalloonsInCategory(c.Id).PagedList(BalloonShopConfiguration.ProductsPerPage, page);

			return View(c);
		}

		public ActionResult Product(string department, string category, string product)
		{
			var balloon = _session.QueryOver<Product>().Where(x => x.Name == product).SingleOrDefault();

			var d = _session.QueryOver<Department> ().Where (x => x.Name == department).SingleOrDefault ();
			var c = _session.QueryOver<Category> ().Where (x => x.Name == category).SingleOrDefault ();

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.DepartmentId = d.Id;
			ViewBag.Categories = _session.CategoriesInDepartment(d.Id).List();

			ViewBag.Category = c;
			ViewBag.CategoryId = c.Id;

			ViewBag.Reviews = new List<ProductReview> { new ProductReview { Name = "Magnus", Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut tristique justo in justo cursus commodo. Sed malesuada scelerisque semper. Cras viverra rutrum nisi in ullamcorper. Sed eu orci magna. Proin rhoncus risus tortor, nec euismod erat. Proin varius accumsan lacus, non egestas nulla vulputate in.", CreatedDate= DateTime.Today } };
			ViewBag.Recomendations = _session.QueryOver<Product>().Where(x => x.Id != balloon.Id).Take(5).List();

			return View(balloon);
		}

		//
		// GET: /Search/
		[CatalogFilter]
		public ActionResult Search(string search, bool allWords, int? page)
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
	}
}

