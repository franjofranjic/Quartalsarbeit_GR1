using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quartalsarbeit_GR1.Models;

namespace Quartalsarbeit_GR1.Controllers
{
    [Authorize(Roles = RoleName.Administrator)]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category kategorie = db.Categories.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Bezeichnung,MinAlter,MaxAlter,Geschlecht")] Category kategorie)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(kategorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategorie);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category kategorie = db.Categories.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // POST: Categories/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Bezeichnung,MinAlter,MaxAlter,Geschlecht")] Category kategorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorie);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category kategorie = db.Categories.Find(id);
            if (kategorie == null)
            {
                return HttpNotFound();
            }
            return View(kategorie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category kategorie = db.Categories.Find(id);
            db.Categories.Remove(kategorie);
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
