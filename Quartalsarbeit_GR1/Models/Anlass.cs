using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Anlass
    {
        public int ID { get; set; }

        public String Bezeichnung { get; set; }

        public String Ort { get; set; }

        public DateTime Datum { get; set; }

        // Teilnehmer müssen noch Startnummer haben
        public List<Athlet> Teilnehmer { get; set; }

        public List<Config> Configs { get; set; }
    }
}