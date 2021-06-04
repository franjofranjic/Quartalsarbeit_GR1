using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Discipline
    {
        public int ID { get; set; }
        public String Bezeichnung { get; set; }

        public String Abkuerzung { get; set; }

        public String Formel { get; set; }
    }
}