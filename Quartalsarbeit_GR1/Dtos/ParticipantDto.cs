using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Dtos
{
    public class ParticipantDto
    {
        public int ID { get; set; }

        public EventDto Event { get; set; }

        public AthleteDto Athlete { get; set; }

        public int StartNumber { get; set; }

        //Evtl noch Wahldisziplinen
    }
}