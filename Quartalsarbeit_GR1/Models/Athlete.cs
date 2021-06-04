using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Athlete
    {
        public int ID { get; set; }

        public String Vorname { get; set; }

        public String Nachname { get; set; }

        public Club Verein { get; set; }

        public DateTime Geburtstag { get; set; }

        public Geschlecht Geschlecht { get; set; }

        public int Gewicht { get; set; }

        public int Groesse { get; set; }

    }
}