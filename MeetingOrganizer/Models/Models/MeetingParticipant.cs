using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeetingOrganizer.Models
{
    public class MeetingParticipant
    {
        [Key]
        public int MeetingParticipantID { get; set; }
        [ForeignKey("Meeting")]
        public int MeetingID { get; set; }
        [ForeignKey("Participant")]
        public int ParticipantID { get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual Participant Participant { get; set; }
    }
}