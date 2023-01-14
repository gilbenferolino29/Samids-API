using Microsoft.EntityFrameworkCore;
using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Services.Impl
{
    public class FacultyService : IFacultyService
    {
        private readonly SamidsDataContext _context;

        public FacultyService(SamidsDataContext context)
        {
            _context = context;
        }
        public async Task<CRUDReturn> AddFaculty(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            _context.SaveChanges();
            return new CRUDReturn 
            { success = true, data = await _context.Faculties.AsNoTracking().ToListAsync() };
        }

        public async Task<CRUDReturn> AddFacultySubjects(FacultySubjectDto<int> request)
        {
            var faculty = await _context.Faculties.Where(f => f.FacultyNo == request.FacultyNo).Include(f=>f.Subjects).AsNoTracking().FirstOrDefaultAsync();
            if(faculty is null)
            {
                return new CRUDReturn
                { success = false, data = FacultyNotFound };
            }
            var subject = await _context.Subjects.FindAsync(request.Subject);
            if (subject is null)
            {
                return new CRUDReturn
                { success = false, data = SubjectNotFound };
            }
            faculty.Subjects.Add(subject);
            _context.Faculties.Update(faculty);
            _context.SaveChanges();
            return new CRUDReturn
            {
                success = true,
                data = faculty
            };
        }
            
        

        public async Task<CRUDReturn> AddFacultySubjects(FacultySubjectDto<List<Subject>> request)
        {
            var faculty = await _context.Faculties.Where(f => f.FacultyNo == request.FacultyNo).Include(f => f.Subjects).AsNoTracking().FirstOrDefaultAsync();
            if (faculty is null)
            {
                return new CRUDReturn
                { success = false, data = FacultyNotFound };
            }

            request.Subject.ForEach(sub => faculty.Subjects.Add(sub));
            _context.Faculties.Update(faculty);
            _context.SaveChanges();
            return new CRUDReturn
            {
                success = true,
                data = faculty
            };
        }

        public async Task<CRUDReturn> DeleteFaculty(int id)
        {
            var faculty = await _context.Faculties.Where(f => f.FacultyNo == id).FirstOrDefaultAsync();
            if (faculty is null) 
            {
                return new CRUDReturn { success = false, data = FacultyNotFound };
            }
            _context.Faculties.Remove(faculty);
            return new CRUDReturn
            { success = true, data = FacultyDelete };
        }

        public async Task<CRUDReturn> GetFaculties()
        {
            return new CRUDReturn 
            { success = true, data = await _context.Faculties.Include(f => f.Subjects).AsNoTracking().ToListAsync() };
        }

        public async Task<CRUDReturn> GetFacultyById(int id)
        {
            var faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.FacultyNo == id);
            if (faculty is null)
            {
                return new CRUDReturn
                { success = false, data = FacultyNotFound };
            }
            return new CRUDReturn { success = true, data = faculty };
        }

        public Task<CRUDReturn> RemoveFacultySubjects(FacultySubjectDto<int> request)
        {
            throw new NotImplementedException();
        }

        public Task<CRUDReturn> RemoveFacultySubjects(FacultySubjectDto<List<Subject>> request)
        {
            throw new NotImplementedException();
        }

        public async Task<CRUDReturn> UpdateFaculty(FacultyUpdateDto request)
        {
            var faculty = await _context.Faculties.Where(f => f.FacultyNo == request.FacultyNo).FirstOrDefaultAsync();
            if (faculty is null)
            {
                return new CRUDReturn { success = false, data = FacultyNotFound };
            }

            faculty.FirstName = request.FirstName;
            faculty.LastName = request.LastName;

            _context.Faculties.Update(faculty);
            _context.SaveChanges();
            return new CRUDReturn
            { success = true, data = faculty };
        }
    }
}
