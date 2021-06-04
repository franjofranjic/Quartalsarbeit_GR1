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
    public class ConfigurationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Configurations
        public IEnumerable<ConfigurationDto> GetConfigurations()
        {
            var configurationsQuery = db.Configurations;



            return configurationsQuery
                .ToList()
                .Select(Mapper.Map<Configuration, ConfigurationDto>);
        }

        // GET: api/Configurations/5
        [ResponseType(typeof(Configuration))]
        public IHttpActionResult GetConfiguration(int id)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            return Ok(configuration);
        }

        // PUT: api/Configurations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConfiguration(int id, Configuration configuration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configuration.ID)
            {
                return BadRequest();
            }

            db.Entry(configuration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigurationExists(id))
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

        // POST: api/Configurations
        [HttpPost]
        public IHttpActionResult PostConfiguration(ConfigurationDto newConfiguration)
        {
            var category = db.Categories.Single(
               c => c.ID == newConfiguration.CategoryId);

            var disciplines = db.Disciplines.Where(
                m => newConfiguration.DisciplineIds.Contains(m.ID)).ToList();

            foreach (var discipline in disciplines)
            {
                var configuration = new Configuration
                {
                    Disziplin = discipline,
                    Kategorie = category,
                };

                db.Configurations.Add(configuration);
            }

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Configurations/5
        public IHttpActionResult DeleteConfiguration(int id)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            db.Configurations.Remove(configuration);
            db.SaveChanges();

            return Ok(configuration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConfigurationExists(int id)
        {
            return db.Configurations.Count(e => e.ID == id) > 0;
        }
    }
}