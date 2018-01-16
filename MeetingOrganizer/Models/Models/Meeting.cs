using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeetingOrganizer.Models
{
    public class Meeting
    {        
        [Key]
        public int MeetingID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public virtual ICollection<MeetingParticipant> mParticipants { get; set; }
    }
}