 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class Verein
    {
        public int ID { get; set; }

        public String Vereinsname { get; set; }

        public String Ort { get; set; }

        public int PLZ { get; set; }

        public String Strasse { get; set; }

        public ApplicationUser Vereinsverantwortlicher { get; set; }

        public ICollection<Athlet> Athleten { get; set; }
    }
}