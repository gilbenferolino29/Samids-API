using Samids_API.Models;

namespace Samids_API.Services.Interfaces
{
    public interface IFacultyService
    {

        Task<IEnumerable<Faculty>> GetFaculties();

        Task<Student> GetFacultyById(int id);

        Task<Student> AddFaculty(Student student);

        Task<Student> UpdateFaculty(Student student);
        Task<Student> DeleteFaculty(int id);
    }
}
