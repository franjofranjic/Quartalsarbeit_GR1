using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class CategoryDto
    {
        public int ID { get; set; }

        public String Bezeichnung { get; set; }

        public int MinAlter { get; set; }

        public int MaxAlter { get; set; }

        public String Geschlecht { get; set; }
    }
}