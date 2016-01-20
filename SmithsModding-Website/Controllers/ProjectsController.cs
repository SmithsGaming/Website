using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmithsModding_Website.Controllers
{
    public class ProjectsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SmithsCore()
        {
            ViewBag.Message = "SmithsCore";

            return View();
        }

        public ActionResult TinyStorage()
        {
            ViewBag.Message = "TinyStorage";

            return View();
        }
    }
}