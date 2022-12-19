using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services;

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
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAll () {
            return Ok(await _attendanceService.GetAttendances());

        }
        [HttpGet]
        [Route("GetAllSA")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllSA(int studentId)
        {
            return Ok(await _attendanceService.GetStudentAttendance( studentId));
        }
        [HttpGet]
        [Route("GetAllFA")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllFA(int facultyId)
        {
            return Ok(await _attendanceService.GetStudFacAttendance( facultyId));
        }

        [HttpPost]
        public async Task<ActionResult<Attendance>> AddAttendance(AddAttendanceDto attendance)
        {
            return Ok(await _attendanceService.AddStudentAttendance(attendance));
        }
    }
}
