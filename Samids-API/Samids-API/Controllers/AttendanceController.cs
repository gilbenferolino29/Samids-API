using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<CRUDReturn>> GetAll() {
            return Ok(await _attendanceService.GetAttendances());

        }
        [HttpGet("{room}")]
        public async Task<ActionResult<CRUDReturn>> GetAllByRoom(string room)
        {
            return Ok(await _attendanceService.GetAttendances(room));

        }
        [HttpGet("{id}")]
        
        public async Task<ActionResult<CRUDReturn>> GetAll(int id)
        {
            return Ok(await _attendanceService.GetAttendances(id));

        }
        [HttpGet("{remarks}")]
        public async Task<ActionResult<CRUDReturn>> GetAll(Remarks remarks)
        {
            return Ok(await _attendanceService.GetAttendances(remarks));

        }






        [HttpGet]
        [Route("GetAllSA")]
        public async Task<ActionResult<CRUDReturn>> GetAllSA(int studentId)
        {
            return Ok(await _attendanceService.GetStudentAttendance(studentId));
        }
        [HttpGet]
        [Route("GetAllFA")]
        public async Task<ActionResult<CRUDReturn>> GetAllFA(int facultyId)
        {
            return Ok(await _attendanceService.GetStudFacAttendance( facultyId));
        }

        [HttpPost]
        public async Task<ActionResult<CRUDReturn>> AddAttendance([FromBody] AddAttendanceDto attendance)
        {
            return Ok(await _attendanceService.AddStudentAttendance(attendance));
        }
    }
}
