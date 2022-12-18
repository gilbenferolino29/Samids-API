using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAll () {
            return Ok(await _attendanceService.GetAttendances());

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllSA(int subjectId, int studentId)
        {
            return Ok(await _attendanceService.GetStudentAttendance(subjectId, studentId));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAllFA(int subjectId, int facultyId)
        {
            return Ok(await _attendanceService.GetStudFacAttendance(subjectId, facultyId));
        }
    }
}
