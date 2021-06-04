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
    public class DisciplinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Disciplines
        public ActionResult Index()
        {
            return View(db.Disciplines.ToList());
        }

        // GET: Disciplines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discipline disziplin = db.Disciplines.Find(id);
            if (disziplin == null)
            {
                return HttpNotFound();
            }
            return View(disziplin);
        }

        // GET: Disciplines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disciplines/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Bezeichnung,Abkuerzung,Formel")] Discipline disziplin)
        {
            if (ModelState.IsValid)
            {
                db.Disciplines.Add(disziplin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disziplin);
        }

        // GET: Disciplines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discipline disziplin = db.Disciplines.Find(id);
            if (disziplin == null)
            {
                return HttpNotFound();
            }
            return View(disziplin);
        }

        // POST: Disciplines/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Bezeichnung,Abkuerzung,Formel")] Discipline disziplin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disziplin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disziplin);
        }

        // GET: Disciplines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discipline disziplin = db.Disciplines.Find(id);
            if (disziplin == null)
            {
                return HttpNotFound();
            }
            return View(disziplin);
        }

        // POST: Disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discipline disziplin = db.Disciplines.Find(id);
            db.Disciplines.Remove(disziplin);
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
