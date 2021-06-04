using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class ConfigViewModel
    {
        public int ID { get; set; }

        public Event Anlass { get; set; }

        public Category Kategorie { get; set; }

        public List<Discipline> Disziplin { get; set; }
    }
}