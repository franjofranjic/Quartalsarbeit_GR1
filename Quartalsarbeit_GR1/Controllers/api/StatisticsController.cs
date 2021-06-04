using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Quartalsarbeit_GR1.Dtos;
using Quartalsarbeit_GR1.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Description;

namespace Quartalsarbeit_GR1.Controllers.api
{
    public class StatisticsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Statistics
        [ResponseType(typeof(StatisticsDto))]
        public IHttpActionResult GetStatistics()
        {
            StatisticsDto statistics = null;
            if (statistics == null)
            {
                return NotFound();
            }

            return Ok(statistics);
        }
    }
}
