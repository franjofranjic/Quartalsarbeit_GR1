using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class EventDto
    {
        public int ID { get; set; }

        public String Bezeichnung { get; set; }

        public String Ort { get; set; }

        public DateTime Datum { get; set; }
    }
}