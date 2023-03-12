using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly ICSVService _csvService;

        public CSVController(ICSVService csvService)
        {
            _csvService = csvService;
        }


        [HttpPost("csvStudent")]
        public async Task<ActionResult<CRUDReturn>> AddStudentFromCSV(string filePath)
        {
            return await _csvService.ReadStudentsFromCsv(filePath);

        }
        [HttpPost("csvSubject")]
        public async Task<ActionResult<CRUDReturn>>AddSubjectFromCSV(string filePath)
        {
            return await _csvService.ReadSubjectsFromCsv(filePath);
        }
        [HttpPost("csvFaculty")]
        public async Task<ActionResult<CRUDReturn>> AddFacultyFromCSV(string filePath)
        {
            return await _csvService.ReadFacultiesFromCsv(filePath);
        }
    }
}
