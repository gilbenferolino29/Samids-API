using Samids_API.Models;

namespace Samids_API.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();

        Task<Student> GetStudentById(int id);

        Task<Student> AddStudent(Student student);

        Task<Student> UpdateStudent(Student student);
        Task<Student> DeleteStudent(int id);

    }
}
