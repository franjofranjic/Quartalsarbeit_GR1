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
    public class DisziplinenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Disziplinen
        public ActionResult Index()
        {
            return View(db.Disziplinen.ToList());
        }

        // GET: Disziplinen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disziplin disziplin = db.Disziplinen.Find(id);
            if (disziplin == null)
            {
                return HttpNotFound();
            }
            return View(disziplin);
        }

        // GET: Disziplinen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Disziplinen/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Bezeichnung,Abkuerzung,Formel")] Disziplin disziplin)
        {
            if (ModelState.IsValid)
            {
                db.Disziplinen.Add(disziplin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(disziplin);
        }

        // GET: Disziplinen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disziplin disziplin = db.Disziplinen.Find(id);
            if (disziplin == null)
            {
                return HttpNotFound();
            }
            return View(disziplin);
        }

        // POST: Disziplinen/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Bezeichnung,Abkuerzung,Formel")] Disziplin disziplin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disziplin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disziplin);
        }

        // GET: Disziplinen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disziplin disziplin = db.Disziplinen.Find(id);
            if (disziplin == null)
            {
                return HttpNotFound();
            }
            return View(disziplin);
        }

        // POST: Disziplinen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disziplin disziplin = db.Disziplinen.Find(id);
            db.Disziplinen.Remove(disziplin);
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
