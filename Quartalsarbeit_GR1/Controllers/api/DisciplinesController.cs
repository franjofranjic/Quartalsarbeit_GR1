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
    public class DisciplinesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Disciplines
        public IHttpActionResult GetDisciplines(string query = null)
        {
            IQueryable<Discipline> disciplinesQuery = db.Disciplines;

            if (!String.IsNullOrWhiteSpace(query))
                disciplinesQuery = disciplinesQuery.Where(c => c.Bezeichnung.Contains(query));

            var disciplineDtos = disciplinesQuery
            .ToList()
            .Select(Mapper.Map<Discipline, DisciplineDto>);

            return Ok(disciplineDtos);
        }

        // GET: api/Disciplines/5
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult GetDiscipline(int id)
        {
            Discipline disziplin = db.Disciplines.Find(id);
            if (disziplin == null)
            {
                return NotFound();
            }

            return Ok(disziplin);
        }

        // PUT: api/Disciplines/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscipline(int id, Discipline disziplin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disziplin.ID)
            {
                return BadRequest();
            }

            db.Entry(disziplin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisziplinExists(id))
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

        // POST: api/Disciplines
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult PostDiscipline(Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Disciplines.Add(discipline);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = discipline.ID }, discipline);
        }

        // DELETE: api/Disciplines/5
        [ResponseType(typeof(Discipline))]
        public IHttpActionResult DeleteDiscipline(int id)
        {
            Discipline disziplin = db.Disciplines.Find(id);
            if (disziplin == null)
            {
                return NotFound();
            }

            db.Disciplines.Remove(disziplin);
            db.SaveChanges();

            return Ok(disziplin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DisziplinExists(int id)
        {
            return db.Disciplines.Count(e => e.ID == id) > 0;
        }
    }
}