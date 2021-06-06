using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartalsarbeit_GR1.Models;

namespace Quartalsarbeit_GR1.Dtos
{
    public class ConfigurationDto
    {
        public EventDto Event { get; set; }

        public CategoryDto Category { get; set; }
        public List<DisciplineDto> Disciplines { get; set; }
    }
}