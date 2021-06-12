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
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Categories
        [HttpGet]
        public IHttpActionResult GetCategories(string query = null)
        {
            IQueryable<Category> categoriesQuery = db.Categories;

            if (!String.IsNullOrWhiteSpace(query))
                categoriesQuery = categoriesQuery.Where(c => c.Bezeichnung.Contains(query));

            var categories = categoriesQuery.ToList();

            var categoryDtos = categoriesQuery
            .ToList()
            .Select(Mapper.Map<Category, CategoryDto>);

            return Ok(categoryDtos);
        }

        // GET: api/Categories/5
        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Category, CategoryDto>(category));
        }

        // PUT: api/Categories/5
        [HttpPut]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.ID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        [HttpPost]
        public IHttpActionResult PostCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var category = Mapper.Map<CategoryDto, Category>(categoryDto);
            db.Categories.Add(category);
            db.SaveChanges();

            categoryDto.ID = category.ID;
            return Created(new Uri(Request.RequestUri + "/" + category.ID), categoryDto);
        }

        // DELETE: api/Categories/5
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(Mapper.Map<Category, CategoryDto>(category));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.ID == id) > 0;
        }
    }
}