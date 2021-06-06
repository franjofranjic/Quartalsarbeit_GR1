using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartalsarbeit_GR1.Models;

namespace Quartalsarbeit_GR1.Dtos
{
    public class DisciplineDto
    {
        public int ID { get; set; }
        public String Bezeichnung { get; set; }

        public String Abkuerzung { get; set; }

        public String Formel { get; set; }
    }
}