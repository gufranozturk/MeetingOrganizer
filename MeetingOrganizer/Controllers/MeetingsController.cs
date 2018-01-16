using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MeetingOrganizer.Models;
using MeetingOrganizer.Models.Context;
using MeetingOrganizer.Models.ViewModels;

namespace MeetingOrganizer.Controllers
{
    public class MeetingsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Meetings
        public ActionResult Index()
        {
            return View(db.Meetings.ToList());
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }

            var Result = from p in db.Participants
                         select new
                         {
                             p.ParticipantID,
                             p.Name,
                             Attendance = ((from mp in db.MeetingsParticipants
                                            where (mp.MeetingID == id) & (mp.ParticipantID == p.ParticipantID)
                                            select mp).Count() > 0)
                         };

            var _MeetingsViewModel = new MeetingsViewModel();
            _MeetingsViewModel.MeetingID = id.Value;
            _MeetingsViewModel.Title = meeting.Title;
            _MeetingsViewModel.Subject = meeting.Subject;
            _MeetingsViewModel.Date = meeting.Date;
            _MeetingsViewModel.StartTime = meeting.StartTime;
            _MeetingsViewModel.EndTime = meeting.EndTime;

            var AttendList = new List<AttendViewModel>();
            foreach (var item in Result)
            {
                AttendList.Add(new AttendViewModel { ID = item.ParticipantID, Name = item.Name, Attend = item.Attendance });
            }

            _MeetingsViewModel.Participants = AttendList;


            return View(_MeetingsViewModel);
        }

        // GET: Meetings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingID,Title,Subject,Date,StartTime,EndTime")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }

            var Result = from p in db.Participants
                         select new
                         {
                             p.ParticipantID,
                             p.Name,
                             Attendance = ((from mp in db.MeetingsParticipants
                                            where (mp.MeetingID == id) & (mp.ParticipantID == p.ParticipantID)
                                            select mp).Count() > 0)
                         };

            var _MeetingsViewModel = new MeetingsViewModel();
            _MeetingsViewModel.MeetingID = id.Value;
            _MeetingsViewModel.Title = meeting.Title;
            _MeetingsViewModel.Subject = meeting.Subject;
            _MeetingsViewModel.Date = meeting.Date;
            _MeetingsViewModel.StartTime = meeting.StartTime;
            _MeetingsViewModel.EndTime = meeting.EndTime;

            var AttendList = new List<AttendViewModel>();
            foreach (var item in Result)
            {
                AttendList.Add(new AttendViewModel { ID = item.ParticipantID, Name = item.Name, Attend = item.Attendance });
            }

            _MeetingsViewModel.Participants = AttendList;


            return View(_MeetingsViewModel);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MeetingsViewModel meeting)
        {
            if (ModelState.IsValid)
            {
                var _Meetings = db.Meetings.Find(meeting.MeetingID);
                _Meetings.Title = meeting.Title;
                _Meetings.Subject = meeting.Subject;
                _Meetings.Date = meeting.Date;
                _Meetings.StartTime = meeting.StartTime;
                _Meetings.EndTime = meeting.EndTime;

                foreach (var item in db.MeetingsParticipants)
                {
                    if (item.MeetingID == meeting.MeetingID)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }
                foreach (var item in meeting.Participants)
                {
                    if (item.Attend)
                    {
                        db.MeetingsParticipants.Add(new MeetingParticipant() { MeetingID = meeting.MeetingID, ParticipantID = item.ID });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
