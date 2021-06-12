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
    //[Authorize(Roles = RoleName.Administrator)]
    public class AthletesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Athletes
        [HttpGet]
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
        [HttpGet]
        public IHttpActionResult GetAthlete(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Athlete, AthleteDto>(athlete));
        }

        // PUT: api/Athletes/5
        [HttpPut]
        public IHttpActionResult PutAthlete(int id, AthleteDto athleteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != athleteDto.ID)
            {
                return BadRequest();
            }

            var athleteInDb = db.Athletes.SingleOrDefault(c => c.ID == id);

            if (athleteInDb == null)
                return NotFound();

            Mapper.Map(athleteDto, athleteInDb);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/Athletes
        [HttpPost]
        public IHttpActionResult PostAthlete(AthleteDto athleteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var athlete = Mapper.Map<AthleteDto, Athlete>(athleteDto);
            db.Athletes.Add(athlete);
            db.SaveChanges();

            athleteDto.ID = athlete.ID;
            return Created(new Uri(Request.RequestUri + "/" + athlete.ID), athleteDto);
        }

        // DELETE: api/Athletes/5
        [HttpDelete]
        public IHttpActionResult DeleteAthlete(int id)
        {
            var athleteInDb = db.Athletes.SingleOrDefault(c => c.ID == id);

            if (athleteInDb == null)
                return NotFound();

            db.Athletes.Remove(athleteInDb);
            db.SaveChanges();

            return Ok();
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