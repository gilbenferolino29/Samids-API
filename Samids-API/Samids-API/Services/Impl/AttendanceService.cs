using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;
using System.Linq;

namespace Samids_API.Services.Impl
{
    public class AttendanceService : IAttendanceService
    {
        private readonly SamidsDataContext _context;
        private readonly int _lateConfig;
        private readonly int _absentConfig;
        public AttendanceService(SamidsDataContext context)
        {
            _context = context;
            _lateConfig = _context.Configs.Single().LateMinutes;
            _absentConfig = _context.Configs.Single().AbsentMinutes;

        }

        public async Task<CRUDReturn> GetRemarksCount()
        {
            var late = (await _context.Attendances.Where(a => a.remarks == Remarks.Late).ToListAsync()).Count;
            var cutting = (await _context.Attendances.Where(a => a.remarks == Remarks.Cutting).ToListAsync()).Count;
            var absent = (await _context.Attendances.Where(a => a.remarks == Remarks.Absent).ToListAsync()).Count;
            var onTime = (await _context.Attendances.Where(a => a.remarks == Remarks.OnTime).ToListAsync()).Count;

           var count = new List<int> { onTime, late, cutting, absent };
            
            return new CRUDReturn
            { success = true, data = count };
        }
        public async Task<CRUDReturn> GetRemarksCount(int studentNo)
        {
            var late = (await _context.Attendances.Where(a => a.remarks == Remarks.Late && a.Student.StudentNo == studentNo).ToListAsync()).Count;
            var cutting = (await _context.Attendances.Where(a => a.remarks == Remarks.Cutting && a.Student.StudentNo == studentNo).ToListAsync()).Count;
            var absent = (await _context.Attendances.Where(a => a.remarks == Remarks.Absent && a.Student.StudentNo == studentNo).ToListAsync()).Count;
            var onTime = (await _context.Attendances.Where(a => a.remarks == Remarks.OnTime && a.Student.StudentNo == studentNo).ToListAsync()).Count;

            var count = new List<int> { onTime, late, cutting, absent };
            return new CRUDReturn
            { success = true, data = count };
        }

        public async Task<CRUDReturn> GetRemarksCount(int studentNo, DateTime date)
        {
            var late = (await _context.Attendances.Where(a => a.remarks == Remarks.Late && a.Student.StudentNo == studentNo && a.Date == date).ToListAsync()).Count;
            var cutting = (await _context.Attendances.Where(a => a.remarks == Remarks.Cutting && a.Student.StudentNo == studentNo && a.Date == date).ToListAsync()).Count;
            var absent = (await _context.Attendances.Where(a => a.remarks == Remarks.Absent && a.Student.StudentNo == studentNo && a.Date == date).ToListAsync()).Count;
            var onTime = (await _context.Attendances.Where(a => a.remarks == Remarks.OnTime && a.Student.StudentNo == studentNo && a.Date == date).ToListAsync()).Count;

            var count = new List<int> { onTime, late, cutting, absent };
            return new CRUDReturn
            { success = true, data = count };
        }
        //GetAttendancesOverload

        public async Task<CRUDReturn> GetAttendances(DateTime date)
        {
            return new CRUDReturn
            { success = true, data = await _context.Attendances.Where(a => a.Date == date).AsNoTracking().ToListAsync() };
        }

        public async Task<CRUDReturn> GetAttendances(DateTime date, int studentNo)
        {
            return new CRUDReturn
            { success = true, data = await _context.Attendances.Where(a => a.Date == date && a.Student.StudentNo == studentNo).AsNoTracking().ToListAsync() };
        }
        public async Task<CRUDReturn> GetAttendances()
        {
            return new CRUDReturn 
            { success = true, data = await _context.Attendances.AsNoTracking().ToListAsync()};
        }
        public async Task<CRUDReturn> GetAttendances(string room)
        {
            return new CRUDReturn 
            { success = true, data = await _context.Attendances.Where(a => a.SubjectSchedule.Room == room).AsNoTracking().ToListAsync() };
        }
        public async Task<CRUDReturn> GetAttendances(string room, Remarks remarks)
        {
            //_context.Attendances.Include(a=>a.SubjectSchedule).AsNoTracking().ToListAsync() - For use if code below doesn't work

            return new CRUDReturn 
            {success = true, data = await _context.Attendances.Where(a=> a.SubjectSchedule.Room == room && a.remarks ==remarks).AsNoTracking().ToListAsync()};
        }

        public async Task<CRUDReturn> GetAttendances(string room, int studentNo)
        {
            //_context.Attendances.Include(a=>a.Student).AsNoTracking().ToListAsync() - For use if code below doesn't work
            return new CRUDReturn 
            { success = true, data = await _context.Attendances.Where(a => a.SubjectSchedule.Room == room && a.Student.StudentNo == studentNo).AsNoTracking().ToListAsync() };
        }
        public async Task<CRUDReturn> GetAttendances(int studentNo)
        {
            return new CRUDReturn 
            { success = true, data = await _context.Attendances.Where(a => a.Student.StudentNo == studentNo).AsNoTracking().ToListAsync() };
        }
        public async Task<CRUDReturn> GetAttendances(int studentNo, Remarks remarks)
        {
            return new CRUDReturn
            { success = true, data = await _context.Attendances.Where(a => a.Student.StudentNo == studentNo && a.remarks == remarks).AsNoTracking().ToListAsync() };
        }
        public async Task<CRUDReturn> GetAttendances(Remarks remarks)
        {
            return new CRUDReturn
            { success = true, data = await _context.Attendances.Where(a => a.remarks == remarks).AsNoTracking().ToListAsync()};
        }
       

        

        

        public async Task<CRUDReturn> GetAttendances(string room, int studentNo, Remarks remarks)
        {
            return new CRUDReturn
            { success = true, data = await _context.Attendances.Where(a => a.SubjectSchedule.Room == room && a.Student.StudentNo == studentNo && a.remarks == remarks).AsNoTracking().ToListAsync() };
        }

        //

        public async Task<CRUDReturn> AddStudentAttendance(AddAttendanceDto attendance)
        {
            var student = await _context.Students.Where(s => s.StudentNo == attendance.studentNo).Include(s=>s.Subjects).ThenInclude(s=>s.SubjectSchedules).SingleAsync();
            //Checks all rooms with schedule on the DayOfTheWeek - ex. Rooms of subjectschedule on Monday
            var schedRoom = await _context.SubjectSchedules.Where(s => s.Room == attendance.room && s.Day == attendance.date.DayOfWeek).ToListAsync();


            //Gets the closest scheduleId based on ActualTimein from Device

            
            var sched = from s in schedRoom let distance = Math.Abs(s.TimeStart.Subtract(attendance.actualTimeIn).Ticks) orderby distance select s;
            
            


            var device = await _context.Devices.SingleOrDefaultAsync(d => d.Room == attendance.room);

            if (student is null)
            {
                return new CRUDReturn 
                { success = false, data = StudentNotFound };   
            }

            //Checks if student really is a student on this room and have the subject given the schedule
            if (await VerifyAttendance(student.Rfid, attendance.room))
            {
                return new CRUDReturn { success=false, data= StudentNotAuthorized};
            }

            //Check Remarks goes here
            var remarks = CheckRemarks(attendance.actualTimeIn, attendance.actualTimeout, sched.FirstOrDefault());

            //Then append to newAttendance
            var newAttendance = new Attendance { Student = student, Date = attendance.date, Device = device, remarks = remarks, SubjectSchedule = sched[0], ActualTimeIn = attendance.actualTimeIn, ActualTimeOut = attendance.actualTimeout };

            _context.Attendances.Add(newAttendance);
            _context.SaveChanges();
            return new CRUDReturn 
            { success = true, data = newAttendance };
        }

        public Remarks CheckRemarks(DateTime timeIn, DateTime timeOut, SubjectSchedule sched)
        {

            var late = sched.TimeStart.AddMinutes(_lateConfig);
            var absent = sched.TimeStart.AddMinutes(_absentConfig);
            var cutting = sched.TimeEnd.AddMinutes(-5);
            if (timeOut.TimeOfDay < cutting.TimeOfDay)
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

        //


        public async Task<CRUDReturn> GetStudentAttendance(int studentId)
        {

            var student = await _context.Students.Where(s => s.StudentNo == studentId).SingleAsync();

            if (student is null)
            {
                return new CRUDReturn { success = false, data = StudentNotFound };
            }
            return new CRUDReturn { success = true, data = await _context.Attendances.Include(a => a.Student).Where(a => a.Student.StudentNo == studentId).AsNoTracking().ToListAsync() };
        }

        //Get All Attendance of Students for Faculty (only assigned subject)
        public async Task<CRUDReturn> GetStudFacAttendance(int facultyId)
        {

            var faculty = await _context.Faculties.Where(f => f.FacultyNo == facultyId).SingleAsync();
            var facSub = await _context.Faculties.Where(f => f.FacultyNo == facultyId).SelectMany(s => s.Subjects).ToListAsync();
            var sched = await _context.SubjectSchedules.Include(s => s.Subject).AsNoTracking().ToListAsync();

            if (faculty is null)
            {
                return new CRUDReturn { success = false, data = FacultyNotFound };
            }

            if (facSub is null)
            {
                return new CRUDReturn { success = false, data = FacultyNotAuthorized };
            }
            Console.WriteLine("1231231");
            var ssSub = from subject in facSub join ss in sched on subject.SubjectID equals ss.Subject.SubjectID select ss;
            Console.Write(ssSub);
            var query = from id in ssSub join atd in _context.Set<Attendance>() on id.SchedId equals atd.SubjectSchedule.SchedId select atd;
            return new CRUDReturn { success = true, data = query };
        }

        public async Task<bool> VerifyAttendance(long rfid, string room)
        {

            //Verify if student exists in the database - Query
            var queryStud = await _context.Students.Include(s => s.Subjects).AsNoTracking().SingleOrDefaultAsync(s => s.Rfid == rfid);
            //Verify if room is assigned to a schedule - Query
            var queryS = await _context.SubjectSchedules.Where(s => s.Room == room).Include(s => s.Subject).AsNoTracking().ToListAsync();

            List<Subject> querySubjects = new() { };



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
