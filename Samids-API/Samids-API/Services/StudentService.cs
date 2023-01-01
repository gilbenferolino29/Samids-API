using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interfaces;

namespace Samids_API.Services
{
    public class StudentService : IStudentService
    {
        private readonly SamidsDataContext _context;

        public StudentService(SamidsDataContext context)
        {
            _context = context;
        }
        public Task<Student> AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> AddStudentSubjects(AddStudentSubjectDto<int> request)
        {
            var student = await _context.Students.Where(s => s.StudentNo == request.StudentNo).Include(s => s.Subjects).FirstOrDefaultAsync();
            if (student is null)
            {
                throw new InvalidOperationException("Student doesn't exist in the database");
            }
            var subject = await _context.Subjects.FindAsync(request.Subject);
            if(subject is null) throw new InvalidOperationException("Subject doesn't exist in the database");

            
            student.Subjects.Add(subject);
            _context.SaveChanges();
            return student;
        }

        public async Task<Student> AddStudentSubjects(AddStudentSubjectDto<List<Subject>> request)
        {
            var student = await _context.Students.Where(s => s.StudentNo == request.StudentNo).Include(s => s.Subjects).FirstOrDefaultAsync();
            if (student is null)
            {
                throw new InvalidOperationException("Student doesn't exist in the database");
            }

            
            var subject = await _context.Subjects.FindAsync(request.Subject);
            if (subject is null) throw new InvalidOperationException("Subject doesn't exist in the database");


            _context.Subjects.AddRange(request.Subject);
            _context.SaveChanges();
            return student;
        }

        public async Task<IEnumerable<Student>> DeleteStudent(int id)
        {
            var student = await _context.Students.Where(s => s.StudentNo == id).FirstOrDefaultAsync();
            if (student is null)
            {
                throw new InvalidOperationException("Student doesn't exist in the database");
            }
            _context.Students.Remove(student);
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s=> s.StudentNo == id);

            if (student is null)
            {
                throw new InvalidOperationException("Student doesn't exist in the database");
            }

            return student;
           
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.Include(s=> s.Subjects).ThenInclude(s=>s.Faculties).AsNoTracking().ToListAsync();
        }

        public async Task<Student> UpdateStudent(Student newStudent)
        {
            var student = await _context.Students.Where(s=>s.StudentNo == newStudent.StudentNo).FirstOrDefaultAsync();
            if(student is null)
            {
                throw new InvalidOperationException("Student doesn't exist in the database");
            }
            _context.Students.Update(student);
            _context.SaveChanges();
            return student;

        }
    }
}
