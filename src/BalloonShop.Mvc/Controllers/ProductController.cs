using System.Web.Mvc;
using NHibernate;
using BalloonShop.Model;
using System.Linq;
using System.Collections.Generic;
using System;
using BalloonShop.Queries;
using System.Web;

namespace BalloonShop.Mvc.Controllers
{
	[ShoppingCartFilter]
    public class ProductController : Controller
    {
        private readonly ISession _session;

        public ProductController(ISession session)
        {
            _session = session;
        }

		public ActionResult Show(string department, string category, string product)
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

    }
}
