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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the official SmithsModding website";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;
            return View();
        }

        public ActionResult News()
        {
            ViewBag.Message = "What's new in the world of Smiths Modding?";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;

            var nim = new NewsItemModels();

            //TODO: Download a given amount of data from the DB and add it into the list. For now empty list:
            nim.Items = new List<NewsItem>();

            nim.newNewsItem = new NewsItem();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact one of the team!";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Add(NewsItemModels model)
        {
            model.newNewsItem.Id = Guid.NewGuid().ToString();
            model.newNewsItem.PublishDate = DateTime.UtcNow;

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model.newNewsItem.Author = await new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(db)).FindByIdAsync(User.Identity.GetUserId());

                db.NewsItems.Add(model.newNewsItem);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("News", "Home");
        }
    }
}