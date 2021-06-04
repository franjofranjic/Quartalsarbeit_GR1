using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public enum Geschlecht
    {
        Männlich, Weiblich
    }
    public class Category
    {
        public int ID { get; set; }

        public String Bezeichnung { get; set; }

        public int MinAlter { get; set; }

        public int MaxAlter { get; set; }

        public Geschlecht Geschlecht { get; set; }
    }
}