using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Teilnehmer
    {
        public int ID { get; set; }

        public Anlass Anlass { get; set; }

        public Teilnehmer Athlet { get; set; }

        //Startnummerblock hmmmm
        public int Startnummer { get; set; }
    }
}