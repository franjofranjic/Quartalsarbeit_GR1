using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class AthleteDto
    {
        public int ID { get; set; }

        public String Vorname { get; set; }

        public String Nachname { get; set; }

        public ClubDto Verein { get; set; }

        public DateTime Geburtstag { get; set; }

        public String Geschlecht { get; set; }

        public int Gewicht { get; set; }

        public int Groesse { get; set; }
    }
}