using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IFacultyService
    {

        Task<CRUDReturn> GetFaculties();

        Task<CRUDReturn> AddFacultySubjects(FacultySubjectDto<int> request);
        Task<CRUDReturn> AddFacultySubjects(FacultySubjectDto<List<Subject>> request);
        Task<CRUDReturn> RemoveFacultySubjects(FacultySubjectDto<int> request);
        Task<CRUDReturn> RemoveFacultySubjects(FacultySubjectDto<List<Subject>> request);
        Task<CRUDReturn> GetFacultyById(int id);

        Task<CRUDReturn> AddFaculty(Faculty faculty);

        Task<CRUDReturn> UpdateFaculty(FacultyUpdateDto request);
        Task<CRUDReturn> DeleteFaculty(int id);
    }
}
