using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmithsModding_Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News()
        {
            ViewBag.Message = "News";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact one of the team!";

            return View();
        }
    }
}