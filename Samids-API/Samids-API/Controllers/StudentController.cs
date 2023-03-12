using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<CRUDReturn>> GetStudents()
        {
            return Ok(await _studentService.GetStudents());
        }

        [HttpGet("ClassesByDay")]
        public async Task<ActionResult<CRUDReturn>> GetStudentClasses([FromBody]DateOnly date, int studentNo)
        {
            return Ok(await _studentService.GetStudentClasses(date, studentNo));
        }

        [HttpGet("ClassesByDate")]
        public async Task<ActionResult<CRUDReturn>> GetStudentClasses([FromBody]DateTime date, int studentNo)
        {
            return Ok(await _studentService.GetStudentClasses(date, studentNo));
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<CRUDReturn>> GetStudentById(int id)
        {
            return Ok(await _studentService.GetStudentById(id));
        }

        [HttpGet("ByRfid/{rfid}")]
        public async Task<ActionResult<CRUDReturn>> GetStudentByRfid(long rfid)
        {
            return Ok(await _studentService.GetStudentByRfid(rfid));
        }

        [HttpPost]
        public async Task<ActionResult<CRUDReturn>> AddStudent (Student student)
        {
            return Ok(await _studentService.AddStudent(student));
        }

        [HttpDelete]
        public async Task<ActionResult<CRUDReturn>> DeleteStudent(int id)
        {
            return Ok(await _studentService.DeleteStudent(id));
        }
        [HttpPatch]
        public async Task<ActionResult<CRUDReturn>> UpdateStudent(StudentUpdateDto request)
        {
            return Ok(await _studentService.UpdateStudent(request));
        }
        [HttpPatch]
        [Route("RemoveSubject")]
        public async Task<ActionResult<CRUDReturn>> RemoveStudentSubject (AddStudentSubjectDto<int> request)
        {
            return Ok(await _studentService.RemoveStudentSubjects(request));
        }

        [HttpPatch]
        [Route("RemoveSubjects")]
        public async Task<ActionResult<CRUDReturn>> RemoveStudentSubject(AddStudentSubjectDto<List<Subject>> request)
        {
            return Ok(await _studentService.RemoveStudentSubjects(request));
        }


        [HttpPatch]
        [Route("AddSubject")]
        public async Task<ActionResult<CRUDReturn>> AddStudentSubject (AddStudentSubjectDto<int> request)
        {
            return Ok(await _studentService.AddStudentSubjects(request));
        }
        [HttpPatch]
        [Route("AddSubjects")]
        public async Task<ActionResult<CRUDReturn>> AddStudentSubject (AddStudentSubjectDto<List<Subject>> request)
        {
            return Ok(await _studentService.AddStudentSubjects(request));
        }
    }
}
