using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quartalsarbeit_GR1.Models;
using Microsoft.AspNet.Identity;


namespace Quartalsarbeit_GR1.Controllers
{
    [Authorize]
    public class AthletesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Athletes
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var verein = db.Clubs.Where(e => e.Vereinsverantwortlicher.Id == userId).FirstOrDefault();
            return View(verein);
        }

        // GET: Athletes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlet = db.Athletes.Find(id);
            if (athlet == null)
            {
                return HttpNotFound();
            }
            return View(athlet);
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Athletes/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlete athlet)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var Verein =  db.Clubs.Where(c => c.Vereinsverantwortlicher.Id == userId).FirstOrDefault();
                athlet.Verein = Verein;
                db.Athletes.Add(athlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(athlet);
        }

        // GET: Athletes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlet = db.Athletes.Find(id);
            if (athlet == null)
            {
                return HttpNotFound();
            }
            return View(athlet);
        }

        // POST: Athletes/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlete athlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(athlet);
        }

        // GET: Athletes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlet = db.Athletes.Find(id);
            if (athlet == null)
            {
                return HttpNotFound();
            }
            return View(athlet);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Athlete athlet = db.Athletes.Find(id);
            db.Athletes.Remove(athlet);
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
