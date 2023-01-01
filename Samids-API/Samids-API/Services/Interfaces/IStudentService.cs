using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();

        Task<Student> AddStudentSubjects(AddStudentSubjectDto<int> request);
        Task<Student> AddStudentSubjects(AddStudentSubjectDto<List<Subject>> request);
        Task<Student> GetStudentById(int id);

        Task<Student> AddStudent(Student student);

        Task<Student> UpdateStudent(Student student);
        Task<IEnumerable<Student>> DeleteStudent(int id);

    }
}
