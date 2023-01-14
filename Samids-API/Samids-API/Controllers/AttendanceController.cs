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
        public async Task<ActionResult<CRUDReturn>> GetAll(string room)
        {
            return Ok(await _attendanceService.GetAttendances(room));
        }
        [HttpGet("{room}&{remarks}")]
        public async Task<ActionResult<CRUDReturn>> GetAll(string room, Remarks remarks)
        {
            return Ok(await _attendanceService.GetAttendances(room, remarks));
        }
        [HttpGet("{room}&{studentNo}")]
        public async Task<ActionResult<CRUDReturn>> GetAll(string room, int studentNo)
        {
            return Ok(await _attendanceService.GetAttendances(room, studentNo));
        }
        [HttpGet("{studentNo}")]
        
        public async Task<ActionResult<CRUDReturn>> GetAll(int studentNo)
        {
            return Ok(await _attendanceService.GetAttendances(studentNo));
        }
        [HttpGet("{studentNo}&{remarks}")]
        public async Task<ActionResult<CRUDReturn>> GetAll(int studentNo, Remarks remarks)
        {
            return Ok(await _attendanceService.GetAttendances(studentNo, remarks));

        }
        [HttpGet("{remarks}")]
        public async Task<ActionResult<CRUDReturn>> GetAll(Remarks remarks)
        {
            return Ok(await _attendanceService.GetAttendances(remarks));
        }
        [HttpGet("{room}&{studentNo}&{remarks}")]
        public async Task<ActionResult<CRUDReturn>> GetAll(string room, int studentNo, Remarks remarks)
        {
            return Ok(await _attendanceService.GetAttendances(room, studentNo, remarks));

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
