using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using BalloonShop.Data;

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

        public ActionResult Show(int id)
        {
            var department = CatalogAccess.GetDepartmentDetails(id);
            return View(department);
        }
    }
}
