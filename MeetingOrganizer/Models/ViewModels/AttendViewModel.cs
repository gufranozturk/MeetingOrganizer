using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingOrganizer.Models.ViewModels
{
    public class AttendViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Attend { get; set; }
    }
}