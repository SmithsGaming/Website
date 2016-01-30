using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmithsModding_Website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmithsModding_Website.Controllers
{
    public class NewsController : Controller
    {

        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            ViewBag.Message = "What's new in the world of Smiths Modding?";
            ViewBag.Logo = "~/Content/res/Logo.jpg";
            ViewBag.Header = true;

            var nim = await getStandardNewsDisplayModel();
            //Return the view.
            return View(nim);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        [ActionName("AddNews")]
        public async System.Threading.Tasks.Task<ActionResult> AddNews(NewsViewModel model)
        {
            if (model.newNewsItem.Title != null && model.newNewsItem.Post != null)
            {
                model.newNewsItem.Id = Guid.NewGuid().ToString();
                model.newNewsItem.PublishDate = DateTime.UtcNow;
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    model.newNewsItem.Author = await new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(db)).FindByIdAsync(User.Identity.GetUserId());
                    db.News.Add(model.newNewsItem);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        [ActionName("EditNews")]
        public async System.Threading.Tasks.Task<ActionResult> EditNews(NewsViewModel model)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model.editNewsItem.Author = await new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(db)).FindByIdAsync(User.Identity.GetUserId());

                db.Entry(model.editNewsItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "News");
        }

        [Authorize(Roles = "Administrators")]
        [ActionName("DeleteNewsItem")]
        public async System.Threading.Tasks.Task<ActionResult> DeleteNewsItem(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                NewsItem news = await db.News.FindAsync(id);
                if (news == null)
                {
                    return HttpNotFound();
                }
                db.News.Remove(news);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "News");
        }

        [Authorize(Roles = "Administrators")]
        [ActionName("GetEditContext")]
        public async System.Threading.Tasks.Task<ActionResult> GetEditContext(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nim = await getStandardNewsDisplayModel();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                NewsItem editItem = await db.News.FindAsync(id);
                if (editItem == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                nim.editNewsItem = editItem;
            }

            return View("Index", nim);
        }

        //GetNewItemContext
        [Authorize(Roles = "Administrators")]
        [ActionName("GetNewItemContext")]
        public async System.Threading.Tasks.Task<ActionResult> GetNewItemContext(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var nim = await getStandardNewsDisplayModel();
            var ni = new NewsItem() { Id = id };

            nim.newNewsItem = ni;

            return View("Index", nim);
        }


        private async System.Threading.Tasks.Task<NewsViewModel> getStandardNewsDisplayModel()
        {
            var nim = new NewsViewModel();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                //Grab only the first 20 post orso.
                int lastPostIndex = 19;

                //If we donnot have 20 posts yet, grab as many as we have.
                if ((await db.News.CountAsync()) < 20)
                {
                    lastPostIndex = (await db.News.CountAsync());
                }

                //Grab the determined amount of posts (use include to grab a relation ship, cause LazyLoading is disabled)
                nim.Items = (await db.News.Include(u => u.Author).ToListAsync()).GetRange(0, lastPostIndex);

                //Sort them based on the release date: mulitply with -1 inverts the sort order
                //That makes it so that the newest post land ontop and the oldest on the bottom.
                nim.Items.Sort((x, y) => x.PublishDate.CompareTo(y.PublishDate) * -1);
            }

            return nim;
        }
    }
}