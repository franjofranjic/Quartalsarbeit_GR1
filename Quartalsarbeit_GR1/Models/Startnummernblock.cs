using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Startnummernblock
    {
        public int ID { get; set; }

        public int minStartnummer { get; set; }
        public int maxStartnummer { get; set; }
        public String gruppierung { get; set; }
        public int differenz { get; set; }

        public Anlass anlass { get; set; }
    }
}