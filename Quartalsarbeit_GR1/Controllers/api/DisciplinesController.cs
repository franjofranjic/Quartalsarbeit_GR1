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
    [Authorize(Roles = RoleName.Administrator)]
    public class DisciplinesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Disciplines
        [HttpGet]
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
        [HttpGet]
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
        [HttpPut]
        public IHttpActionResult PutDiscipline(int id, Discipline discipline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discipline.ID)
            {
                return BadRequest();
            }

            db.Entry(discipline).State = EntityState.Modified;

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
        [HttpPost]
        public IHttpActionResult PostDiscipline(DisciplineDto disciplinedto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var discipline = Mapper.Map<DisciplineDto, Discipline>(disciplinedto);
            db.Disciplines.Add(discipline);
            db.SaveChanges();

            disciplinedto.ID = discipline.ID;
            return Created(new Uri(Request.RequestUri + "/" + discipline.ID), disciplinedto);
        }

        // DELETE: api/Disciplines/5
        [HttpDelete]
        public IHttpActionResult DeleteDiscipline(int id)
        {
            Discipline disziplin = db.Disciplines.Find(id);
            if (disziplin == null)
            {
                return NotFound();
            }

            db.Disciplines.Remove(disziplin);
            try
            {
                db.SaveChanges();
            }
            catch {
                return BadRequest("Es besteht abhängige Konfigurationen mit der Disziplin");
            }

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