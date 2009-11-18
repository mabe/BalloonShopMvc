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
    public class DepartmentController : Controller
    {

        public ActionResult Navigation(int? DepartmentId)
        {
            ViewData["DepartmentId"] = DepartmentId ?? 0;
            var departments = CatalogAccess.GetDepartments();

            return View(departments);
        }

        public ActionResult Show(int id, int? page)
        {
            int howManyPages;
            var department = CatalogAccess.GetDepartmentDetails(id);
            var balloons = CatalogAccess.GetProductsOnDepartmentPromotion(department.Id, page ?? 1, out howManyPages);

            department.PromotedBalloons = new PagedList<Balloon>(page ?? 1, BalloonShopConfiguration.ProductsPerPage, howManyPages, balloons);

            return View(department);
        }
    }
}
