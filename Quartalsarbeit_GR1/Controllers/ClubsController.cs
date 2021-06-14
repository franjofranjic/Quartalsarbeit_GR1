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
    public class ClubsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clubs
        public ActionResult Index()
        {
            var vereine = db.Clubs.ToList();
            return View(db.Clubs.ToList());
        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club verein = db.Clubs.Find(id);
            if (verein == null)
            {
                return HttpNotFound();
            }
            return View(verein);
        }

        // GET: Clubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Vereinsname,Ort,PLZ,Strasse")] Club verein)
        {
            if (ModelState.IsValid)
            {
                db.Clubs.Add(verein);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(verein);
        }

        // GET: Clubs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club verein = db.Clubs.Find(id);
            if (verein == null)
            {
                return HttpNotFound();
            }
            return View(verein);
        }

        // POST: Clubs/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Vereinsname,Ort,PLZ,Strasse,Vereinsveratnwortlicher")] Club verein)
        {
            if (ModelState.IsValid)
            {
                var vereinsverantwortlicher = verein.Vereinsverantwortlicher;
                db.Entry(verein).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(verein);
        }

        // GET: Clubs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club verein = db.Clubs.Find(id);
            if (verein == null)
            {
                return HttpNotFound();
            }
            return View(verein);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club verein = db.Clubs.Find(id);
            db.Clubs.Remove(verein);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Athletes/Index/5
        public ActionResult Athletes(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                var verein = db.Clubs.Find(id);
                return View("Athletes", verein);
            }
        }

        // GET: Clubs/Delete/5
        public ActionResult AthleteDelete(int? id)
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
        [HttpPost, ActionName("AthleteDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AthleteDeleteConfirmed(int id)
        {
            Athlete athlet = db.Athletes.Find(id);
            db.Athletes.Remove(athlet);
            db.SaveChanges();
            return RedirectToAction("Athletes");
        }

        // POST: Clubs/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AthleteEdit([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlete athlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Athletes");
            }
            return View(athlet);
        }

        // GET: Athletes/Create
        public ActionResult AthleteEdit(int? id)
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
        public ActionResult AthleteCreate()
        {
            return View();
        }

        // POST: Athletes/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AthleteCreate([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlete athlet)
        {
            if (ModelState.IsValid)
            {
                // Hardcoded Ya Salame
                var vereinsId = 7;
                var Verein = db.Clubs.Where(c => c.ID == vereinsId).FirstOrDefault();
                athlet.Verein = Verein;
                db.Athletes.Add(athlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(athlet);
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
