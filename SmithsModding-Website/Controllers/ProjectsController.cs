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
            ViewBag.Message = "Find links to all of our projects below!";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            return View();
        }

        public ActionResult SmithsCore()
        {
            ViewBag.Message = "The super mod required for all SmithsModding projects and a framework for other mods!";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            return View();
        }

        public ActionResult Armory()
        {
            ViewBag.Message = "Armor - No longer a useless defence!";
            ViewBag.Logo = "~/Content/res/ArmoryLogo.png";
            return View();
        }

        public ActionResult Weaponry()
        {
            ViewBag.Message = "Tinkers Construct addon for Armory!";
            ViewBag.Logo = "~/Content/res/WeaponryLogo.png";
            return View();
        }

        public ActionResult World()
        {
            ViewBag.Message = "World Generation addon for Armory!";
            ViewBag.Logo = "~/Content/res/WorldLogo.png";
            return View();
        }

        public ActionResult TinyStorage()
        {
            ViewBag.Message = "Smaller, cuter storage options!";
            ViewBag.Logo = "~/Content/res/TinyStorageLogo.png";
            return View();
        }
    }
}