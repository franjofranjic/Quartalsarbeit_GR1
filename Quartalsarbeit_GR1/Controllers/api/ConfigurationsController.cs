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
    public class ConfigurationsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Configurations
        [HttpGet]
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
        [Route("api/Configurations/addCategory")]
        public IHttpActionResult AddCategory(Configuration newConfiguration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            newConfiguration = new Configuration
            {
                Anlass = db.Events.Find(newConfiguration.Anlass.ID),
                Disziplin = db.Disciplines.Find(newConfiguration.Disziplin.ID),
                Kategorie = db.Categories.Find(newConfiguration.Kategorie.ID),
            };
            db.Configurations.Add(newConfiguration);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(ConfigurationDto configurationDto)
        {
            var categoryID = db.Categories.Single(
                          c => c.ID == configurationDto.Category.ID).ID;

            var eventsID = db.Events.Single(
                c => c.ID == configurationDto.Event.ID).ID;

            foreach (var disciplineDto in configurationDto.Disciplines)
            {
                Configuration configuration = db.Configurations.Where(c => c.Anlass.ID == eventsID && c.Disziplin.ID == disciplineDto.ID && c.Kategorie.ID == categoryID).FirstOrDefault();

                if(configuration != null)
                    db.Configurations.Remove(configuration);

            }

            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Configurations/5
        [HttpDelete]
        [Route("api/Configurations/deleteConfig")]
        public IHttpActionResult DeleteConfiguration(Configuration delConfiguration)
        {
            Configuration configuration = db.Configurations.Where(c => c.Anlass.ID == delConfiguration.Anlass.ID && c.Disziplin.ID == delConfiguration.Disziplin.ID && c.Kategorie.ID == delConfiguration.Kategorie.ID).FirstOrDefault();
            if (configuration == null)
            {
                return NotFound();
            }

            db.Configurations.Remove(configuration);
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