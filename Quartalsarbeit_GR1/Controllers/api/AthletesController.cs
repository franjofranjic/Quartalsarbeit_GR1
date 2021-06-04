using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Quartalsarbeit_GR1.Models;
using Quartalsarbeit_GR1.Dtos;
using AutoMapper;

namespace Quartalsarbeit_GR1.Controllers.api
{
    public class AthletesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Athletes
        public IEnumerable<AthleteDto> GetAthletes()
        {
            var athletesQuery = db.Athletes;

            return athletesQuery
                .Include(c => c.Verein)
                .Include(c => c.Verein.Vereinsverantwortlicher)
                .ToList()
                .Select(Mapper.Map<Athlete, AthleteDto>);
        }

        // GET: api/Athletes/5
        [ResponseType(typeof(Athlete))]
        public IHttpActionResult GetAthlete(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return NotFound();
            }

            return Ok(athlete);
        }

        // PUT: api/Athletes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAthlete(int id, Athlete athlete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != athlete.ID)
            {
                return BadRequest();
            }

            db.Entry(athlete).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Athletes
        [ResponseType(typeof(Athlete))]
        public IHttpActionResult PostAthlete(Athlete athlete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Athletes.Add(athlete);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = athlete.ID }, athlete);
        }

        // DELETE: api/Athletes/5
        [ResponseType(typeof(Athlete))]
        public IHttpActionResult DeleteAthlete(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return NotFound();
            }

            db.Athletes.Remove(athlete);
            db.SaveChanges();

            return Ok(athlete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AthleteExists(int id)
        {
            return db.Athletes.Count(e => e.ID == id) > 0;
        }
    }
}