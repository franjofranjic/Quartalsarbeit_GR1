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
        public List<ConfigurationDto> GetConfigurations(int id = 0)
        {
            var configurationsGrouped = db.Configurations
                            .Include(c => c.Anlass)
                            .Include(c => c.Kategorie)
                            .Include(c => c.Disziplin)
                            .ToList()
                            .GroupBy(c => new
                            {
                                c.Kategorie,
                                c.Anlass
                            }).ToList();

            if (id != 0)
                configurationsGrouped = db.Configurations.Include(c => c.Anlass).Include(c => c.Kategorie).Include(c => c.Disziplin)
                            .Where(c => c.Anlass.ID == id).ToList()
                            .GroupBy(c => new
                            {
                                c.Kategorie,
                                c.Anlass
                            }).ToList();

            var configDtos = new List<ConfigurationDto>();
            
            foreach (var config in configurationsGrouped) {
                configDtos.Add(new ConfigurationDto
                {
                    Event = Mapper.Map<Event, EventDto>(config.Key.Anlass),
                    Category = Mapper.Map<Category, CategoryDto>(config.Key.Kategorie),
                    Disciplines = Mapper.Map<List<Discipline>, List<DisciplineDto>>(config.Select(c => c.Disziplin).ToList())
                });
            }

            return configDtos;
        }

        // GET: api/Configurations/5
        [ResponseType(typeof(ConfigurationDto))]
        public IHttpActionResult GetConfiguration(int id)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Configuration, ConfigurationDto>(configuration));
        }

        // PUT: api/Configurations/5
        [HttpPut]
        public IHttpActionResult PutConfiguration(int id, ConfigurationDto configurationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configurationDto.ID)
            {
                return BadRequest();
            }

            var configurationInDb = db.Configurations.SingleOrDefault(c => c.ID == id);

            if (configurationInDb == null)
                return NotFound();

            Mapper.Map(configurationDto, configurationInDb);
            db.SaveChanges();

            return Ok();
        }

        // POST: api/Configurations
        [HttpPost]
        public IHttpActionResult PostConfiguration(ConfigurationDto newConfiguration)
        {
            var category = db.Categories.Single(
               c => c.ID == newConfiguration.Category.ID);

            var events = db.Events.Single(
                c => c.ID == newConfiguration.Event.ID);

            var disciplines = new List<Discipline>();

            foreach (var disciplineDto in newConfiguration.Disciplines) {
                disciplines.Add(Mapper.Map<DisciplineDto, Discipline>(disciplineDto));
               
            }

            foreach (var discipline in disciplines)
            {
                var configuration = new Configuration
                {
                    Anlass = events,
                    Disziplin = discipline,
                    Kategorie = category,
                };

                db.Configurations.Add(configuration);
            }

            db.SaveChanges();

            return Ok();
        }

        // POST: api/Configurations/addCategory
        [HttpPost]
        public IHttpActionResult AddCategory(ConfigurationDto newConfiguration)
        {

            
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
    }
}