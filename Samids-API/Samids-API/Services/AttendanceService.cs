using Samids_API.Data;
using Samids_API.Models;

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

        public async Task<IEnumerable<Attendance>> GetStudentAttendance(int subjectId, int studentId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            var student = await _context.Students.FindAsync(studentId);

            if(subject is null && student is null)
            {
                throw new InvalidOperationException("Subject or Student does not exist!");
            }
            return  await _context.Attendances.Where(a => a.SubjectSchedule.Subject.SubjectID == subjectId && a.Student.StudentID == studentId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Attendance>> GetStudFacAttendance(int subjectId, int facultyId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            var faculty = await _context.Faculties.FindAsync(facultyId);

            var query = await _context.Attendances.Where(a => a.SubjectSchedule.Subject.SubjectID == subjectId && a.SubjectSchedule.Subject.Faculties.Where(f => f.FacultyId == facultyId) != null).AsNoTracking().ToListAsync();

            if (subject is null && faculty is null)
            {
                throw new InvalidOperationException("Subject or Faculty does not exist!");
            }

            if(query is null)
            {
                throw new InvalidOperationException("Faculty may not have access or not assigned to this subject");
            }

            return query;
        }

        public async Task<bool> VerifyAttendance(long rfid, string room)
        {

            //Verify if student exists in the database - Query
            var queryStud = await _context.Students.AsNoTracking().SingleOrDefaultAsync(s => s.Rfid == rfid);
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
