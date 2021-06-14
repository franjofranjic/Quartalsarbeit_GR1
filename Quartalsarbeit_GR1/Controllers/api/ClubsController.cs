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
    public class ClubsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Clubs
        [HttpGet]
        public IEnumerable<ClubDto> GetClubs()
        {
            var clubsQuery = db.Clubs;
            var clubs = clubsQuery.Include(e => e.Vereinsverantwortlicher).ToList();

            return clubsQuery
                .ToList()
                .Select(Mapper.Map<Club, ClubDto>);
        }

        // GET: api/Clubs/5
        [HttpGet]
        public IHttpActionResult GetClub(int id)
        {
            Club club = db.Clubs.Find(id);
            if (club == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Club, ClubDto>(club));
        }

        // PUT: api/Clubs/5
        [HttpPut]
        public IHttpActionResult PutClub(int id, ClubDto clubDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clubDto.ID)
            {
                return BadRequest();
            }

            var clubInDb = db.Clubs.SingleOrDefault(c => c.ID == id);

            if (clubInDb == null)
                return NotFound();

            Mapper.Map(clubDto, clubInDb);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/Clubs
        [HttpPost]
        public IHttpActionResult PostClub(ClubDto clubDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var club = Mapper.Map<ClubDto, Club>(clubDto);
            db.Clubs.Add(club);
            db.SaveChanges();

            clubDto.ID = club.ID;
            return Created(new Uri(Request.RequestUri + "/" + club.ID), clubDto);
        }

        // DELETE: api/Clubs/5
        [HttpDelete]
        public IHttpActionResult DeleteClub(int id)
        {
            var clubsInDb = db.Clubs.SingleOrDefault(c => c.ID == id);


            if (clubsInDb == null)
                return NotFound();

            db.Athletes.RemoveRange(db.Athletes.Where(a => a.Verein.ID == clubsInDb.ID));
            db.SaveChanges();

            db.Clubs.Remove(clubsInDb);
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