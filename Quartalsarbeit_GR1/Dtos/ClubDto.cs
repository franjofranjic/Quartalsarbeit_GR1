using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class ClubDto
    {
        public int ID { get; set; }

        public String Vereinsname { get; set; }

        public String Ort { get; set; }

        public int PLZ { get; set; }

        public String Strasse { get; set; }

        //Hier würde ich halt den ganzen Benutze kennnen
        public VereinsverantwortlicherDto Vereinsverantwortlicher { get; set; }
    }
}