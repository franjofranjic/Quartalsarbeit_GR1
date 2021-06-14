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
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Events
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.Administrator))
            {
                return View(db.Events.ToList());
            }
            else
            {
                return View("ReadOnlyList", db.Events.ToList());
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event anlass = db.Events.Find(id);
            if (anlass == null)
            {
                return HttpNotFound();
            }
            return View(anlass);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Bezeichnung,Ort,Datum")] Event anlass)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(anlass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anlass);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event anlass = db.Events.Find(id);
            if (anlass == null)
            {
                return HttpNotFound();
            }
            return View(anlass);
        }

        // POST: Events/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Bezeichnung,Ort,Datum")] Event anlass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anlass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anlass);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event anlass = db.Events.Find(id);
            if (anlass == null)
            {
                return HttpNotFound();
            }
            return View(anlass);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event anlass = db.Events.Find(id);
            db.Events.Remove(anlass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Events/Statistik/
        public ActionResult Statistics(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DataPoint> dataPoints = new List<DataPoint>();
            /*
            Anlass anlass = db.Events.Find(id);
            var teilnehmer = anlass.Participants;
            var config = anlass.Configurations;

            
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

        // GET: Events/Participants/5
        public ActionResult Participants(int? id)
        {
            var Event = db.Events.Where(e => e.ID == id).FirstOrDefault();

            return View(Event);
        }

        // GET: Events/Configuration/5
        public ActionResult Configuration(int? id)
        {
            var Event = db.Events.Where(e => e.ID == id).FirstOrDefault();

            return View(Event);
        }

        public ActionResult addCategory(int? id)
        {
            var Event = db.Events.Where(e => e.ID == id).FirstOrDefault();

            return View(Event);
        }

        // POST: Events/Addkategorie
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addCategory([Bind(Include = "ID,Anlass,Kategorie,Disziplin")] Configuration config)
        {
            if (ModelState.IsValid)
            {
                db.Configurations.Add(config);
                db.SaveChanges();
                return RedirectToAction("Konfiguration");
            }

            return View(config);
        }

        // POST: Events/Addkategorie
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participants(List<Participant> teilnehmerList)
        {
            if (ModelState.IsValid)
            {
                foreach (var teilnehmer in teilnehmerList) {
                    db.Participants.Add(teilnehmer);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teilnehmerList);
        }



        // GET: Events/Startnummeblockr/5
        public ActionResult StartNumberConfiguration(int? id)
        {
            StartNumberConfiguration startnummernblock = db.StartNumberConfigurations.Where(e => e.anlass.ID == id).First();
            return View(startnummernblock);
        }

        // GET: Events/Startnummeblockr/5
        public ActionResult StartNumberConfigurationEdit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StartNumberConfiguration startnummernblock = db.StartNumberConfigurations.Where(e => e.anlass.ID == id).First();
            if (startnummernblock == null)
            {
                return HttpNotFound();
            }
            return View(startnummernblock);
        }

        // POST: Events/Edit/5
        // Aktivieren Sie zum Schutz vor Angriffen durch Overposting die jeweiligen Eigenschaften, mit denen eine Bindung erfolgen soll. 
        // Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartNumberConfigurationEdit([Bind(Include = "ID,minStartnummer,maxStartnummer,gruppierung,differenz")] StartNumberConfiguration startnummernblock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startnummernblock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(startnummernblock);
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
