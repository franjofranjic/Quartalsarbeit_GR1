using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
    public class ConfigViewModel
    {
        public int ID { get; set; }

        public Anlass Anlass { get; set; }

        public Kategorie Kategorie { get; set; }

        public List<Disziplin> Disziplin { get; set; }
    }
}