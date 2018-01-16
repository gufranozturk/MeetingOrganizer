using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeetingOrganizer.Models.Context
{
    public class MyDbContext:DbContext
    {
        public MyDbContext():base("MyDbContext")
        {

        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<MeetingParticipant> MeetingsParticipants { get; set; }
    }
}