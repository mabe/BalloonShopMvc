using BalloonShop.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BalloonShop.Queries;

namespace BalloonShop.Admin.Mvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ISession _session;

        public CatalogController(ISession session)
        {
            _session = session;
        }

        public ActionResult Departments(int? id) {
            ViewBag.Department = id.HasValue || id == 0 ? _session.Get<Department>(id.Value) : new Department();

            return View(_session.QueryOver<Department>().List());
        }

        [HttpPost]
        public ActionResult Departments(int id, string name, string description)
        {
            var department = _session.Get<Department>(id) ?? new Department();

            department.Name = name;
            department.Description = description;

            if (id == 0) {
                _session.Save(department);
            }

            return RedirectToAction("Departments", new { id = department.Id });
        }

        public ActionResult Categories(int? id, int departmentid) {
            ViewBag.Category = id.HasValue || id == 0 ? _session.Get<Category>(id.Value) : new Category();
            ViewBag.DepartmentId = departmentid;

            return View(_session.CategoriesInDepartment(departmentid).List());
        }

        [HttpPost]
        public ActionResult Categories(int id, int departmentid, string name, string description) {
            var category = _session.Get<Category>(id) ?? new Category();

            category.Name = name;
            category.Description = description;
            category.Department = _session.Load<Department>(departmentid);

            if (id == 0) {
                _session.Save(category);
            }

            return RedirectToAction("Categories", new { id = category.Id, departmentid = departmentid });
        }

        public ActionResult Products(int? id, int categoryid) {
            var category = _session.Get<Category>(categoryid);

            ViewBag.CategoryId = categoryid;
            ViewBag.DepartmentId = category.Department.Id;

            return View(_session.BalloonsInCategory(categoryid).List());
        }

        public ActionResult Product(int id, int categoryid) {
            var product = _session.Get<Balloon>(id);

            ViewBag.CurrentCategoryId = categoryid;
            ViewBag.CategoriesWithProduct = product.Categories.ToList();
            ViewBag.CategoriesWithoutProduct = _session.QueryOver<Category>().Where(Restrictions.On<Category>(x => x.Id).Not.IsIn(product.Categories.Select(x => x.Id).ToArray())).List();

            return View(product);
        }
    }
}
