using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Participant
    {
        public int ID { get; set; }

        public Event Event { get; set; }

        public Athlete Athlete { get; set; }

        public int StartNumber { get; set; }
    }
}