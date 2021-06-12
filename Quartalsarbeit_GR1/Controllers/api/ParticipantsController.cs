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
    public class ParticipantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Participants
        public IEnumerable<ParticipantDto> GetParticipants(int id)
        {
            var Event = db.Events.Find(id);



            var participants = db.Participants
                  .Include(c => c.Event)
                  .Include(c => c.Athlete)
                  .Include(c => c.Athlete.Verein)
                  .ToList();

            var athletes = db.Athletes
               .Include(c => c.Verein).ToList();
            athletes.RemoveAll(x => participants.Any(y => y.Athlete.ID == x.ID));


            var participantDtos = new List<ParticipantDto>();

            foreach (var participant in participants) {
                participantDtos.Add(new ParticipantDto
                {
                    ID = participant.ID,
                    Teilnahme = true,
                    Event = Mapper.Map<Event, EventDto>(participant.Event),
                    Athlete = Mapper.Map<Athlete, AthleteDto>(participant.Athlete),
                    StartNumber = participant.StartNumber,
                });
            }




            foreach (var athlete in athletes)
            {
                participantDtos.Add(new ParticipantDto
                {
                    Teilnahme = false,
                    Event = Mapper.Map<Event, EventDto>(Event),
                    Athlete = Mapper.Map<Athlete, AthleteDto>(athlete),
                });
            }


            return participantDtos;
        }

        // PUT: api/Participants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipant(int id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.ID)
            {
                return BadRequest();
            }

            db.Entry(participant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // POST: api/Participants
        [ResponseType(typeof(Participant))]
        public IHttpActionResult PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Participants.Add(participant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = participant.ID }, participant);
        }

        // DELETE: api/Participants/5
        [ResponseType(typeof(Participant))]
        public IHttpActionResult DeleteParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            db.Participants.Remove(participant);
            db.SaveChanges();

            return Ok(participant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipantExists(int id)
        {
            return db.Participants.Count(e => e.ID == id) > 0;
        }
    }
}