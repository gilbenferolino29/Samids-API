using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Models;
using Samids_API.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return Ok(await _studentService.GetStudents());
        }
    }
}
