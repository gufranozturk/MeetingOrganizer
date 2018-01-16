using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingOrganizer.Models.ViewModels
{
    public class MeetingsViewModel
    {
        public int MeetingID { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<AttendViewModel> Participants { get; set; }
    }
}