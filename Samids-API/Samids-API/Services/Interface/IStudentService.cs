using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IStudentService
    {
        Task<CRUDReturn> GetStudents();

        Task<CRUDReturn> AddStudentSubjects(AddStudentSubjectDto<int> request);
        Task<CRUDReturn> AddStudentSubjects(AddStudentSubjectDto<List<Subject>> request);

        Task<CRUDReturn> RemoveStudentSubjects(AddStudentSubjectDto<int> request);
        Task<CRUDReturn> RemoveStudentSubjects(AddStudentSubjectDto<List<Subject>> request);
        Task<CRUDReturn> GetStudentById(int id);

        Task<CRUDReturn> AddStudent(Student student);

        Task<CRUDReturn> UpdateStudent(StudentUpdateDto student);
        Task<CRUDReturn> DeleteStudent(int id);

    }
}
