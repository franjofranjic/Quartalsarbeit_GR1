using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Participant
    {
        public int ID { get; set; }

        public Event Anlass { get; set; }

        public Athlete Athlet { get; set; }

        //Startnummerblock hmmmm
        public int Startnummer { get; set; }
    }
}