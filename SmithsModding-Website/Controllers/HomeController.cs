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

        public async System.Threading.Tasks.Task<ActionResult> News()
        {
            ViewBag.Message = "What's new in the world of Smiths Modding?";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;

            var nim = new NewsViewModel();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //Grab only the first 20 post orso.
                int lastPostIndex = 19;

                //If we donnot have 20 posts yet, grab as many as we have.
                if ((await db.NewsItems.CountAsync()) < 20)
                {
                    lastPostIndex = (await db.NewsItems.CountAsync());
                }

                //Grab the determined amount of posts (use include to grab a relation ship, cause LazyLoading is disabled)
                nim.Items = (await db.NewsItems.Include(u => u.Author).ToListAsync()).GetRange(0, lastPostIndex);

                //Sort them based on the release date: mulitply with -1 inverts the sort order
                //That makes it so that the newest post land ontop and the oldest on the bottom.
                nim.Items.Sort((x, y) => x.PublishDate.CompareTo(y.PublishDate) * -1);
            }

            //Return the view.
            return View(nim);
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
        public async System.Threading.Tasks.Task<ActionResult> AddNews(NewsViewModel model)
        {
            if (model.newNewsItem.Title != null && model.newNewsItem.Post != null)
            {
                model.newNewsItem.Id = Guid.NewGuid().ToString();
                model.newNewsItem.PublishDate = DateTime.UtcNow;
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    model.newNewsItem.Author = await new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(db)).FindByIdAsync(User.Identity.GetUserId());
                    db.NewsItems.Add(model.newNewsItem);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("News", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                NewsItem news = await db.NewsItems.FindAsync(id);
                if(news == null)
                {
                    return HttpNotFound();
                }
                db.NewsItems.Remove(news);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("News", "Home");
        }
    }
}