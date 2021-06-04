using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class ConfigurationDto
    {
        public int EventId { get; set; }

        public int CategoryId { get; set; }
        public List<int> DisciplineIds { get; set; }
    }
}