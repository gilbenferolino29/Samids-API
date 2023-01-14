using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Services.Impl
{
    public class StudentService : IStudentService
    {
        private readonly SamidsDataContext _context;

        public StudentService(SamidsDataContext context)
        {
            _context = context;
        }
        public async Task<CRUDReturn> AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return new CRUDReturn 
            { success = true, data = await _context.Students.AsNoTracking().ToListAsync() }; 
        }

        public async Task<CRUDReturn> AddStudentSubjects(AddStudentSubjectDto<int> request)
        {
            var student = await _context.Students.Where(s => s.StudentNo == request.StudentNo).Include(s => s.Subjects).FirstOrDefaultAsync();
            if (student is null)
            {
                return new CRUDReturn 
                { success = false, data = StudentNotFound };
            }
            var subject = await _context.Subjects.FindAsync(request.Subject);
            if (subject is null)
            {
                return new CRUDReturn 
                { success = false, data = SubjectNotFound };
            }


            student.Subjects.Add(subject);
            _context.Students.Update(student);
            _context.SaveChanges();
            return new CRUDReturn 
            { success = true, data=student};
        }

        public async Task<CRUDReturn> AddStudentSubjects(AddStudentSubjectDto<List<Subject>> request)
        {
            var student = await _context.Students.Where(s => s.StudentNo == request.StudentNo).Include(s => s.Subjects).FirstOrDefaultAsync();
            if (student is null)
            {
                return new CRUDReturn 
                {success= false, data = StudentNotFound};
            }


            
            request.Subject.ForEach(sub => student.Subjects.Add(sub));
            _context.SaveChanges();
            return new CRUDReturn 
            { success = true, data = student };
        }

        public async Task<CRUDReturn> RemoveStudentSubjects(AddStudentSubjectDto<int> request)
        {
            var student = await _context.Students.Where(s => s.StudentNo == request.StudentNo).Include(s => s.Subjects).FirstOrDefaultAsync();
            if (student is null)
            {
                return new CRUDReturn
                { success = false, data = StudentNotFound };
            }
            var subject = await _context.Subjects.FindAsync(request.Subject);
            if (subject is null)
            {
                return new CRUDReturn
                { success = false, data = SubjectNotFound };
            }


            student.Subjects.Remove(subject);
            _context.SaveChanges();
            return new CRUDReturn
            { success = true, data = student };
        }

        public async Task<CRUDReturn> RemoveStudentSubjects(AddStudentSubjectDto<List<Subject>> request)
        {
            var student = await _context.Students.Where(s => s.StudentNo == request.StudentNo).Include(s => s.Subjects).FirstOrDefaultAsync();
            if (student is null)
            {
                return new CRUDReturn
                { success = false, data = StudentNotFound };
            }


            var subject = await _context.Subjects.FindAsync(request.Subject);
            if (subject is null)
            {
                return new CRUDReturn
                { success = false, data = SubjectNotFound };

            }
            request.Subject.ForEach(sub => student.Subjects.Remove(sub));
            _context.SaveChanges();
            return new CRUDReturn
            { success = true, data = student };
        }

        public async Task<CRUDReturn> DeleteStudent(int id)
        {
            var student = await _context.Students.Where(s => s.StudentNo == id).FirstOrDefaultAsync();
            if (student is null)
            {
                return new CRUDReturn 
                { success= false, data = StudentNotFound};
            }
            _context.Students.Remove(student);
            return new CRUDReturn 
            { success = true, data = StudentDelete };
        }

        public async Task<CRUDReturn> GetStudentById(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNo == id);

            if (student is null)
            {
                return new CRUDReturn 
                { success = false, data = StudentNotFound };
            }

            return new CRUDReturn 
            { success = true, data = student };

        }

        public async Task<CRUDReturn> GetStudents()
        {
            return new CRUDReturn 
            { success = true, data = await _context.Students.Include(s => s.Subjects).ThenInclude(s => s.Faculties).AsNoTracking().ToListAsync() };
        }

        public async Task<CRUDReturn> UpdateStudent(StudentUpdateDto request)
        {
            var student = await _context.Students.Where(s => s.StudentNo ==request.StudentNo).FirstOrDefaultAsync();
            if (student is null)
            {
                return new CRUDReturn { success= false, data = StudentNotFound };
            }
            student.Year = request.year;
            student.FirstName = request.FirstName; 
            student.LastName = request.LastName;
            student.Course = request.Course;

            _context.Students.Update(student);
            _context.SaveChanges();
            return new CRUDReturn { success = true, data = student };

        }

        public async Task<CRUDReturn> GetStudentClasses(DateTime date, int studentNo)
        {

            var subjects = await _context.Students.Where(s => s.StudentNo == studentNo).SelectMany(s => s.Subjects).ToListAsync();
            var sched = await _context.SubjectSchedules.Include(s => s.Subject).AsNoTracking().ToListAsync();
            if(subjects is null)
            {
                return new CRUDReturn
                {
                    success = false,
                    data = StudentNotFound
                };
            }

            var schedule = from subject in subjects join ss in sched on subject.SubjectID equals ss.Subject.SubjectID where ss.TimeStart > date select ss;

            return new CRUDReturn
            { success = true, data = schedule };
        }

        public async Task<CRUDReturn> GetStudentClasses(DateOnly date, int studentNo)
        {
            var subjects = await _context.Students.Where(s => s.StudentNo == studentNo).SelectMany(s => s.Subjects).ToListAsync();
            var sched = await _context.SubjectSchedules.Include(s => s.Subject).AsNoTracking().ToListAsync();

            if (subjects is null)
            {
                return new CRUDReturn
                {
                    success = false,
                    data = StudentNotFound
                };
            }

            var schedule = from subject in subjects join ss in sched on subject.SubjectID equals ss.Subject.SubjectID where ss.Day > date.DayOfWeek select ss;

            return new CRUDReturn
            { success = true, data = schedule };
        }
    }
}
