using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quartalsarbeit_GR1.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace Quartalsarbeit_GR1.Controllers
{
    public class AnlaesseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anlaesse
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Administrator))
            {
                return View(db.Anlaesse.ToList());
            }
            else {
                return View("ReadOnlyList", db.Anlaesse.ToList());
            }
        }

        // GET: Anlaesse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anlass anlass = db.Anlaesse.Find(id);
            if (anlass == null)
            {
                return HttpNotFound();
            }
            return View(anlass);
        }

        // GET: Anlaesse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anlaesse/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Bezeichnung,Ort,Datum")] Anlass anlass)
        {
            if (ModelState.IsValid)
            {
                db.Anlaesse.Add(anlass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anlass);
        }

        // GET: Anlaesse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anlass anlass = db.Anlaesse.Find(id);
            if (anlass == null)
            {
                return HttpNotFound();
            }
            return View(anlass);
        }

        // POST: Anlaesse/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Bezeichnung,Ort,Datum")] Anlass anlass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anlass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anlass);
        }

        // GET: Anlaesse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anlass anlass = db.Anlaesse.Find(id);
            if (anlass == null)
            {
                return HttpNotFound();
            }
            return View(anlass);
        }

        // POST: Anlaesse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anlass anlass = db.Anlaesse.Find(id);
            db.Anlaesse.Remove(anlass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Anlaesse/Statistik/
        public ActionResult Statistik(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DataPoint> dataPoints = new List<DataPoint>();
            /*
            Anlass anlass = db.Anlaesse.Find(id);
            var teilnehmer = anlass.Teilnehmer;
            var config = anlass.Configs;

            
            foreach (var configItem in config) {
                var kategorie = configItem.Kategorie;
                var filtered = teilnehmer.FindAll(e =>
                DateTime.Now.Subtract(e.Geburtstag).TotalDays / 365 < kategorie.MaxAlter &&
                DateTime.Now.Subtract(e.Geburtstag).TotalDays / 365 < kategorie.MaxAlter &&
                e.Geschlecht == kategorie.Geschlecht);
                dataPoints.Add(new DataPoint(configItem.Kategorie.Bezeichnung, filtered.Count));

            }
            */


            dataPoints.Add(new DataPoint("JKU16", 13));
            dataPoints.Add(new DataPoint("JKU14", 8));
            dataPoints.Add(new DataPoint("JMU18", 7));
            dataPoints.Add(new DataPoint("JMU16", 5));
            dataPoints.Add(new DataPoint("JMU14", 5));
           
            return View(dataPoints); 
        }

        // GET: Anlaesse/Teilnehmer/5
        public ActionResult Teilnehmer(int? id)
        {
            var athleten = db.Athleten.ToList();
            var teilnehmer = db.Teilnehmer.Where(e => e.Anlass.ID == id).ToList();
            //Todo der Teilnehmer besitzt auch Wahldisziplinen in einem zweiten Schritt

            var modelList = new List<TeilnehmerViewModel>();

            foreach (var athlet in athleten) {
                modelList.Add(new TeilnehmerViewModel
                {
                    athlet = athlet,
                    teilnahme = teilnehmer.Any(e => e.Athlet.ID == athlet.ID)
                }) ;
            } 

            if (!User.IsInRole(RoleName.Administrator)) {
                var userId = User.Identity.GetUserId();
                Verein Verein = db.Vereine.Where(e => e.Vereinsverantwortlicher.Id == userId).First();
                foreach (var model in modelList) {

                    var vereinId = model.athlet;
                }
                var filteredModelList = modelList.Where(e => e.athlet.Verein.ID == Verein.ID).ToList();
            }

            return View(modelList);
        }

        // GET: Anlaesse/Konfiguration/5
        public ActionResult Konfiguration(int? id)
        {
            var configs = db.Configs.Where(e => e.Anlass.ID == id).ToList();
            var categories = db.Kategorien.ToList();
            var disciplines = db.Disziplinen.ToList();

            var tupleModel = new Tuple<List<Config>, List<Kategorie>, List<Disziplin>>(configs, categories, disciplines);

            return View(tupleModel);
        }

        // GET: Anlaesse/Startnummer/5
        public ActionResult Startnummer(int? id)
        {
             

            return View();
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
