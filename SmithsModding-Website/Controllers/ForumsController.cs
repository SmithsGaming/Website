using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmithsModding_Website.Controllers
{
    public class ForumsController : Controller
    {
        // GET: Forums
        public ActionResult Index()
        {
            return View();
        }
    }
}