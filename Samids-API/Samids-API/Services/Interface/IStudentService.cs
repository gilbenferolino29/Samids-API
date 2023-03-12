using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IStudentService
    {
        //static Task<CRUDReturn> CheckRFID(long id);
        Task<CRUDReturn> GetStudents();
        Task<CRUDReturn> GetStudentClasses(DateTime date, int studentNo);
        Task<CRUDReturn> GetStudentClasses(DateOnly date, int studentNo);
        //Task<CRUDReturn> GetStudentsClasses(DateTime date);
        //Task<CRUDReturn> GetStudentClasses(DateTime date);
        Task<CRUDReturn> GetStudentById(int id);
        Task<CRUDReturn> GetStudentByRfid(long id);
        Task<CRUDReturn> AddStudentSubjects(AddStudentSubjectDto<int> request);
        Task<CRUDReturn> AddStudentSubjects(AddStudentSubjectDto<List<Subject>> request);
        Task<CRUDReturn> RemoveStudentSubjects(AddStudentSubjectDto<int> request);
        Task<CRUDReturn> RemoveStudentSubjects(AddStudentSubjectDto<List<Subject>> request);
        Task<CRUDReturn> AddStudent(Student student);
        Task<CRUDReturn> UpdateStudent(StudentUpdateDto student);
        Task<CRUDReturn> DeleteStudent(int id);


    }
}
