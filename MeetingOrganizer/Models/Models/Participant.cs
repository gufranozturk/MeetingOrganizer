using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeetingOrganizer.Models
{
    public class Participant
    {        
        [Key]
        public int ParticipantID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MeetingParticipant> pMeetings { get; set; }
    }
}