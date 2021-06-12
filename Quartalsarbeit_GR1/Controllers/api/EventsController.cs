

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
    public class EventsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Events
        [HttpGet]
        public IEnumerable<EventDto> GetEvents()
        {
            var eventsQuery = db.Events;

            return eventsQuery
                .ToList()
                .Select(Mapper.Map<Event, EventDto>);
        }

        // GET: api/Events/5
        [HttpGet]
        public IHttpActionResult GetEvent(int id)
        {
            var Event = db.Events.SingleOrDefault(c => c.ID == id);

            if (Event == null)
                return NotFound();

            return Ok(Mapper.Map<Event, EventDto>(Event));
        }

        // PUT: api/Events/5
        [HttpPut]
        public IHttpActionResult PutEvent(int id, EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var eventInDb = db.Events.SingleOrDefault(c => c.ID == id);

            if (eventInDb == null)
                return NotFound();

            Mapper.Map(eventDto, eventInDb);

            db.SaveChanges();

            return Ok();
        }

        // POST: api/Events
        [HttpPost]
        public IHttpActionResult PostEvent(EventDto eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var Event = Mapper.Map<EventDto, Event>(eventDto);
            db.Events.Add(Event);
            db.SaveChanges();

            eventDto.ID = Event.ID;
            return Created(new Uri(Request.RequestUri + "/" + Event.ID), eventDto);
        }

        // DELETE: api/Events/5
        [HttpDelete]
        public IHttpActionResult DeleteEvent(int id)
        {
            var eventInDb = db.Events.SingleOrDefault(c => c.ID == id);

            if (eventInDb == null)
                return NotFound();

            db.Events.Remove(eventInDb);
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