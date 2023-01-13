using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IAttendanceService
    {
        Task<CRUDReturn> GetAttendances();
        Task<CRUDReturn> GetAttendances(string room);
        Task<CRUDReturn> GetAttendances(int studentId);
        Task<CRUDReturn> GetAttendances(Remarks remarks);
        //Get All Attendance of Students for Faculty (only assigned subject)
        Task<CRUDReturn> GetStudFacAttendance(int facultyId);

        //Verifies Student Attendance if it aligns with Subject - Room - Schedule
        Task<bool> VerifyAttendance(long rfid, string room);

        //Get All Attendance of Students per Subject
        Task<CRUDReturn> GetStudentAttendance(int studentId);

        //After Verification add attendance
        Task<CRUDReturn> AddStudentAttendance(AddAttendanceDto attendance);

        Remarks CheckRemarks(DateTime timeIn, DateTime timeOut, SubjectSchedule sched);


    }
}
