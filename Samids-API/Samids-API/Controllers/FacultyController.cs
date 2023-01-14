using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Impl;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }


        [HttpGet]
        public async Task<ActionResult<CRUDReturn>> GetFaculties()
        {
            return Ok(await _facultyService.GetFaculties());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CRUDReturn>> GetFacultyById(int id)
        {
            return Ok(await _facultyService.GetFacultyById(id));
        }

        [HttpPost]
        public async Task<ActionResult<CRUDReturn>> AddStudent(Faculty faculty)
        {
            return Ok(await _facultyService.AddFaculty(faculty));
        }

        [HttpDelete]
        public async Task<ActionResult<CRUDReturn>> DeleteFaculty(int id)
        {
            return Ok(await _facultyService.DeleteFaculty(id));
        }
        [HttpPatch]
        public async Task<ActionResult<CRUDReturn>> UpdateFaculty(FacultyUpdateDto request)
        {
            return Ok(await _facultyService.UpdateFaculty(request));
        }
        [HttpPatch]
        [Route("RemoveSubject")]
        public async Task<ActionResult<CRUDReturn>> RemoveFacultysSubject(FacultySubjectDto<int> request)
        {
            return Ok(await _facultyService.RemoveFacultySubjects(request));
        }

        [HttpPatch]
        [Route("RemoveSubjects")]
        public async Task<ActionResult<CRUDReturn>> RemoveFacultySubject(FacultySubjectDto<List<Subject>> request)
        {
            return Ok(await _facultyService.RemoveFacultySubjects(request));
        }


        [HttpPatch]
        [Route("AddSubject")]
        public async Task<ActionResult<CRUDReturn>> AddFacultySubject(FacultySubjectDto<int> request)
        {
            return Ok(await _facultyService.AddFacultySubjects(request));
        }
        [HttpPatch]
        [Route("AddSubjects")]
        public async Task<ActionResult<CRUDReturn>> AddFacultySubject(FacultySubjectDto<List<Subject>> request)
        {
            return Ok(await _facultyService.AddFacultySubjects(request));
        }
    }
}
