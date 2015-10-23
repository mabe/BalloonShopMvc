using System.Web.Mvc;
using NHibernate;
using BalloonShop.Model;
using System.Linq;
using System.Collections.Generic;
using System;
using BalloonShop.Queries;

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

        public ActionResult Show(int id, int? categoryid, int? departmentid)
        {
            var balloon = _session.Get<Product>(id);

			departmentid = departmentid ?? balloon.Categories.Select (x => x.Department.Id).FirstOrDefault ();

			ViewBag.Departments = _session.QueryOver<Department>().List();
			ViewBag.DepartmentId = departmentid;
			ViewBag.Categories = _session.CategoriesInDepartment(departmentid.Value).List();
            
			var category = categoryid.HasValue && categoryid != 0 
				? balloon.Categories.FirstOrDefault(x => x.Id == categoryid) 
				: balloon.Categories.FirstOrDefault(x => x.Department.Id == departmentid);

			ViewBag.Category = category;
			ViewBag.CategoryId = category.Id;

            ViewBag.Reviews = new List<ProductReview> { new ProductReview { Name = "Magnus", Review = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut tristique justo in justo cursus commodo. Sed malesuada scelerisque semper. Cras viverra rutrum nisi in ullamcorper. Sed eu orci magna. Proin rhoncus risus tortor, nec euismod erat. Proin varius accumsan lacus, non egestas nulla vulputate in.", CreatedDate= DateTime.Today } };
            ViewBag.Recomendations = _session.QueryOver<Product>().Where(x => x.Id != id).Take(5).List();

            return View(balloon);
        }

    }
}
