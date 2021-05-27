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
    public class AthletenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Athleten
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var verein = db.Vereine.Where(e => e.Vereinsverantwortlicher.Id == userId).FirstOrDefault();
            var athletenVonVerein = db.Athleten.Where(e => e.Verein.ID == verein.ID).ToList();
            return View(athletenVonVerein);
        }

        // GET: Athleten/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlet athlet = db.Athleten.Find(id);
            if (athlet == null)
            {
                return HttpNotFound();
            }
            return View(athlet);
        }

        // GET: Athleten/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Athleten/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlet athlet)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var Verein =  db.Vereine.Where(c => c.Vereinsverantwortlicher.Id == userId).FirstOrDefault();
                athlet.Verein = Verein;
                db.Athleten.Add(athlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(athlet);
        }

        // GET: Athleten/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlet athlet = db.Athleten.Find(id);
            if (athlet == null)
            {
                return HttpNotFound();
            }
            return View(athlet);
        }

        // POST: Athleten/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlet athlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(athlet);
        }

        // GET: Athleten/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlet athlet = db.Athleten.Find(id);
            if (athlet == null)
            {
                return HttpNotFound();
            }
            return View(athlet);
        }

        // POST: Athleten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Athlet athlet = db.Athleten.Find(id);
            db.Athleten.Remove(athlet);
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
