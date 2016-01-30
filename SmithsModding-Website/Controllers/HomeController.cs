using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmithsModding_Website.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace SmithsModding_Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the official SmithsModding website";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact one of the team!";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;
            return View();
        }

        
    }
}