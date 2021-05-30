﻿using System;
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
    public class VereineController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vereine
        public ActionResult Index()
        {
            var vereine = db.Vereine.ToList();
            return View(db.Vereine.ToList());
        }

        // GET: Vereine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verein verein = db.Vereine.Find(id);
            if (verein == null)
            {
                return HttpNotFound();
            }
            return View(verein);
        }

        // GET: Vereine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vereine/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Vereinsname,Ort,PLZ,Strasse")] Verein verein)
        {
            if (ModelState.IsValid)
            {
                db.Vereine.Add(verein);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(verein);
        }

        // GET: Vereine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verein verein = db.Vereine.Find(id);
            if (verein == null)
            {
                return HttpNotFound();
            }
            return View(verein);
        }

        // POST: Vereine/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Vereinsname,Ort,PLZ,Strasse,Vereinsveratnwortlicher")] Verein verein)
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

        // GET: Vereine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verein verein = db.Vereine.Find(id);
            if (verein == null)
            {
                return HttpNotFound();
            }
            return View(verein);
        }

        // POST: Vereine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verein verein = db.Vereine.Find(id);
            db.Vereine.Remove(verein);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Athleten/Index/5
        public ActionResult Athleten(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                var verein = db.Vereine.Find(id);
                var athletenVonVerein = db.Athleten.Where(e => e.Verein.ID == verein.ID).ToList();
                return View("Athleten", athletenVonVerein);
            }
        }

        // GET: Vereine/Delete/5
        public ActionResult AthletDelete(int? id)
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
        [HttpPost, ActionName("AthletDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AthletDeleteConfirmed(int id)
        {
            Athlet athlet = db.Athleten.Find(id);
            db.Athleten.Remove(athlet);
            db.SaveChanges();
            return RedirectToAction("Athleten");
        }

        // POST: Vereine/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AthletEdit([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlet athlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Athleten");
            }
            return View(athlet);
        }

        // GET: Athleten/Create
        public ActionResult AthletEdit(int? id)
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
        public ActionResult AthletCreate()
        {
            return View();
        }

        // POST: Athleten/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AthletCreate([Bind(Include = "ID,Vorname,Nachname,Geburtstag,Geschlecht,Gewicht,Groesse")] Athlet athlet)
        {
            if (ModelState.IsValid)
            {
                // Hardcoded Ya Salame
                var vereinsId = 7;
                var Verein = db.Vereine.Where(c => c.ID == vereinsId).FirstOrDefault();
                athlet.Verein = Verein;
                db.Athleten.Add(athlet);
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
