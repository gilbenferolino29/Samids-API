﻿using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using System.Linq;

namespace Samids_API.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly SamidsDataContext _context;
        private readonly int _lateConfig;
        private readonly int _absentConfig;
        public AttendanceService(SamidsDataContext context) {
            _context = context;
            _lateConfig = _context.Configs.Single().LateMinutes;
            _absentConfig = _context.Configs.Single().AbsentMinutes;

        }
        public async Task<Attendance> AddStudentAttendance(AddAttendanceDto attendance)
        {
            var student = await _context.Students.FindAsync(attendance.studentId);
            //Checks all rooms with schedule on the DayOfTheWeek - ex. Rooms of subjectschedule on Monday
            var schedRoom = await _context.SubjectSchedules.Where(s => s.Room == attendance.room && s.Day == attendance.date.DayOfWeek).ToListAsync();
            //Gets the closest scheduleId based on ActualTimein from Device
            var sched = (from s in schedRoom let distance = Math.Abs(s.TimeStart.Subtract(attendance.actualTimeIn).Ticks) orderby distance select s).First();
            
            var device = await _context.Devices.SingleOrDefaultAsync(d=> d.Room == attendance.room);

            
            //Checks if student really is a student on this room and have the subject given the schedule
            if(await VerifyAttendance(student.Rfid, attendance.room) is not true)
            {
                return null;
            }

            //Check Remarks goes here
            var remarks = CheckRemarks(attendance.actualTimeIn, attendance.actualTimeout, sched);
            
            //Then append to newAttendance



            var newAttendance = new Attendance { Student = student, Date = attendance.date, Device = device, remarks = remarks,SubjectSchedule = sched, ActualTimeIn = attendance.actualTimeIn, ActualTimeOut = attendance.actualTimeout };

            _context.Attendances.Add(newAttendance);
            _context.SaveChanges();
            return newAttendance;
        }

        public Remarks CheckRemarks(DateTime timeIn, DateTime timeOut, SubjectSchedule sched)
        {

           var late = sched.TimeStart.AddMinutes(_lateConfig);
           var absent = sched.TimeStart.AddMinutes(_absentConfig);
           var cutting = sched.TimeEnd.AddMinutes(-5);
           if(timeOut.TimeOfDay < cutting.TimeOfDay)
           {
             return Remarks.Cutting;
           }
            if (timeIn.TimeOfDay > absent.TimeOfDay)
            {
                return Remarks.Absent;
            }
            else if (timeIn.TimeOfDay > late.TimeOfDay)
            {
                return Remarks.Late;
            }

            return Remarks.OnTime;

                
            
           
        }

        //Please check this function - Adotac for time verification purpose remarks

        //public Task<Remarks> CheckRemarks(DateTime time, SubjectSchedule sched)
        //{

        //    if(time.TimeOfDay < sched.TimeStart.TimeOfDay)
        //    {

        //    }
        //}

        public async Task<IEnumerable<Attendance>> GetAttendances()
        {
            return await _context.Attendances.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetStudentAttendance(int studentId)
        {
            
            var student = await _context.Students.FindAsync(studentId);

            if( student is null)
            {
                throw new InvalidOperationException("Student does not exist!");
            }
            return  await _context.Attendances.Where(a =>  a.Student.StudentID == studentId).AsNoTracking().ToListAsync();
        }

        //Get All Attendance of Students for Faculty (only assigned subject)
        public async Task<IEnumerable<Attendance>> GetStudFacAttendance(int facultyId)
        {
           
            var faculty = await _context.Faculties.FindAsync(facultyId);
            var facSub = await _context.Faculties.Where(f => f.FacultyId == facultyId).SelectMany(s => s.Subjects).ToListAsync();
            
            if  (faculty is null)
            {
                throw new InvalidOperationException("Faculty does not exist!");
            }

            if(facSub is null)
            {
                throw new InvalidOperationException("Faculty may not have access or not assigned to any subject");
            }

            var ssSub = from subject in facSub join ss in _context.Set<SubjectSchedule>() on subject.SubjectID equals ss.Subject!.SubjectID select new { ss };
            var query = from id in ssSub join atd in _context.Set<Attendance>() on id.ss.SchedId equals atd.SubjectSchedule.SchedId select atd;
            return query;
        }

        public async Task<bool> VerifyAttendance(long rfid, string room)
        {

            //Verify if student exists in the database - Query
            var queryStud = await _context.Students.Include(s=>s.Subjects).AsNoTracking().SingleOrDefaultAsync(s => s.Rfid == rfid);
            //Verify if room is assigned to a schedule - Query
            var queryS = await _context.SubjectSchedules.Where(s=>s.Room == room).AsNoTracking().ToListAsync();

            List<Subject> querySubjects = new(){};
           
            

            if (queryStud is null)
            {
                throw new InvalidOperationException("Student doesn't exist in the database");
            }
            if (queryS is null)
            {
                throw new InvalidOperationException("No subject/schedule assigned for this room");
            }

            foreach (SubjectSchedule subjectSchedule in queryS)
            {
                if (subjectSchedule.Room == room)
                {
                    foreach (Subject subject in queryStud.Subjects)
                    {

                        if (subject.SubjectID == subjectSchedule.Subject.SubjectID)
                        {
                            querySubjects.Add(subject);
                        }
                    }
                }
            }

            if (querySubjects.Any())
            {
                return true;
            }
            return false;


        }
    }
}
