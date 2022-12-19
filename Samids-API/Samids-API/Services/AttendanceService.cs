using Samids_API.Data;
using Samids_API.Models;
using System.Linq;

namespace Samids_API.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly SamidsDataContext _context;

        public AttendanceService(SamidsDataContext context) {
            _context = context;
        }
        public async Task<Attendance> AddStudentAttendance(Attendance attendance)
        {
            if(await VerifyAttendance(attendance.Student.Rfid, attendance.SubjectSchedule.Room) is not true)
            {
                return null;
            }

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return attendance;
        }

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
            var ssSub = from subject in facSub join ss in _context.Set<SubjectSchedule>() on subject.SubjectID equals ss.Subject!.SubjectID select new { ss };
            var query = from id in ssSub join atd in _context.Set<Attendance>() on id.ss.SchedId equals atd.SubjectSchedule.SchedId select atd;
            
            if  (faculty is null)
            {
                throw new InvalidOperationException("Faculty does not exist!");
            }

            if(facSub is null)
            {
                throw new InvalidOperationException("Faculty may not have access or not assigned to any subject");
            }

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
