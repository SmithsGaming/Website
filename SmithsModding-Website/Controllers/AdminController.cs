using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmithsModding_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmithsModding_Website.Controllers
{

    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Message = "Modify settings for the site";
            return View();
        }

        public ActionResult RoleConfig()
        {
            RoleConfigurationModels rc = new RoleConfigurationModels();
            rc.Roles = (new RoleManager<IdentityRole, string>).Roles.ToList();
            ViewBag.Message = "Configure different user roles";
            return View(rc);
        }

        public ActionResult UserRoles()
        {
            ViewBag.Message = "Assign roles to users";
            return View();
        }
    }
}