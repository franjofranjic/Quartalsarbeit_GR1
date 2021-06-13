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
    public class ParticipantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Participants
        [HttpGet]
        [Route("api/Participants/{eventID}")]
        public IEnumerable<ParticipantDto> GetParticipants(int eventID)
        {
            var Event = db.Events.Find(eventID);

            var participants = db.Participants
                  .Include(c => c.Event)
                  .Include(c => c.Athlete)
                  .Include(c => c.Athlete.Verein)
                  .Where(c => c.Event.ID == eventID)
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

        // POST: api/Participants
        [HttpPost]
        [Route("api/Participants/{eventID}")]
        public IHttpActionResult PostParticipant(List<ParticipantDto> participantDtos, int eventID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var participants = db.Participants.Where(e => eventID == e.Event.ID).ToList();
            db.Participants.RemoveRange(participants);
            db.SaveChanges();
            participantDtos.RemoveAll(x => x.Teilnahme == false);
            var newParticipants = Mapper.Map<List<ParticipantDto>, List<Participant>>(participantDtos);
            foreach (var participant in newParticipants) {
                db.Participants.Add(new Participant{
                    Event = db.Events.Find(participant.Event.ID),
                    Athlete = db.Athletes.Find(participant.Athlete.ID),
                    StartNumber = participant.StartNumber,
                });
            }
            db.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/"), participantDtos);
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