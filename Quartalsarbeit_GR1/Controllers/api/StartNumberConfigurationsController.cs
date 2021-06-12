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
    public class StartNumberConfigurationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/StartNumberConfigurations
        [HttpGet]
        public IHttpActionResult GetStartNumberConfigurations()
        {
            var startNumberConfigurationQuery = db.StartNumberConfigurations
                .Include(c => c.anlass);

    

            var startNumberConfigurationDtos = startNumberConfigurationQuery
                .ToList()
                .Select(Mapper.Map<StartNumberConfiguration, StartNumberConfigurationDto>);

            return Ok(startNumberConfigurationDtos);
        }

        // GET: api/StartNumberConfigurations/5
        [HttpGet]
        public IHttpActionResult GetStartNumberConfiguration(int id)
        {
            StartNumberConfiguration startNumberConfiguration = db.StartNumberConfigurations.Find(id);
            if (startNumberConfiguration == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<StartNumberConfiguration, StartNumberConfigurationDto>(startNumberConfiguration));
        }

        // PUT: api/StartNumberConfigurations/5
        [HttpPut]
        public IHttpActionResult PutStartNumberConfiguration(int id, StartNumberConfigurationDto startNumberConfigurationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var startNumberConfigurationInDb = db.StartNumberConfigurations.SingleOrDefault(c => c.ID == id);

            if (startNumberConfigurationInDb == null)
                return NotFound();

            Mapper.Map(startNumberConfigurationDto, startNumberConfigurationInDb);

            db.SaveChanges();

            return Ok();
        }

        // POST: api/StartNumberConfigurations
        [HttpPost]
        public IHttpActionResult PostStartNumberConfiguration(StartNumberConfigurationDto startNumberConfigurationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var startNumberConfiguration = Mapper.Map<StartNumberConfigurationDto, StartNumberConfiguration>(startNumberConfigurationDto);
            db.StartNumberConfigurations.Add(startNumberConfiguration);
            db.SaveChanges();

            startNumberConfigurationDto.ID = startNumberConfiguration.ID;
            return Created(new Uri(Request.RequestUri + "/" + startNumberConfiguration.ID), startNumberConfigurationDto);
        }

        // DELETE: api/StartNumberConfigurations/5
        [HttpDelete]
        public IHttpActionResult DeleteStartNumberConfiguration(int id)
        {
            var startNumberConfigurationInDb = db.StartNumberConfigurations.SingleOrDefault(c => c.ID == id);

            if (startNumberConfigurationInDb == null)
                return NotFound();

            db.StartNumberConfigurations.Remove(startNumberConfigurationInDb);
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