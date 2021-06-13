using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class VereinsverantwortlicherDto
    {
        public String Id { get; set; }

        public String Vorname { get; set; }

        public String Nachname { get; set; }

        public string TelPrivat { get; set; }

        public string TelGeschaeft { get; set; }

        public String Email { get; set; }
        public String UserName { get; set; }

    }
}