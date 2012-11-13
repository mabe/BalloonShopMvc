using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BalloonShop.Mvc.Controllers
{
    public class OrderController : Controller
    {
        
        public ActionResult Placed()
        {
            ViewBag.HideCartNavigation = true;

            return View();
        }

    }
}
