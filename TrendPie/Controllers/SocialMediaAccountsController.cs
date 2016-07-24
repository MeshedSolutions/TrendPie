using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TrendPie.Models;

namespace TrendPie.Controllers
{
    public class SocialMediaAccountsController : Controller
    {
        private TrendPie_dbEntities db = new TrendPie_dbEntities();

        // GET: SocialMediaAccounts
        public ActionResult Index()
        {
            return View(db.SocialMediaAccounts.ToList());
        }

        // GET: SocialMediaAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaAccount socialMediaAccount = db.SocialMediaAccounts.Find(id);
            if (socialMediaAccount == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaAccount);
        }

        // GET: SocialMediaAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SocialMediaAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserID,AccountType,UserName,ProfileName,NumberOfTweets,NumberOfFollowering,NumberOfFollowers,DateCreated")] SocialMediaAccount socialMediaAccount)
        {
            if (ModelState.IsValid)
            {
                db.SocialMediaAccounts.Add(socialMediaAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(socialMediaAccount);
        }

        // GET: SocialMediaAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaAccount socialMediaAccount = db.SocialMediaAccounts.Find(id);
            if (socialMediaAccount == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaAccount);
        }

        // POST: SocialMediaAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserID,AccountType,UserName,ProfileName,NumberOfTweets,NumberOfFollowering,NumberOfFollowers,DateCreated")] SocialMediaAccount socialMediaAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMediaAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialMediaAccount);
        }

        // GET: SocialMediaAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialMediaAccount socialMediaAccount = db.SocialMediaAccounts.Find(id);
            if (socialMediaAccount == null)
            {
                return HttpNotFound();
            }
            return View(socialMediaAccount);
        }

        // POST: SocialMediaAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialMediaAccount socialMediaAccount = db.SocialMediaAccounts.Find(id);
            db.SocialMediaAccounts.Remove(socialMediaAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
