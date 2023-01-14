using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IAttendanceService
    {
        Task<CRUDReturn> GetAttendances();

        Task<CRUDReturn> GetAttendances(DateTime date);
        Task<CRUDReturn> GetAttendances(DateTime date, int studentNo);

        Task<CRUDReturn> GetAttendances(string room);
        Task<CRUDReturn> GetAttendances(string room, Remarks remarks);
        Task<CRUDReturn> GetAttendances(string room, int studentNo); 
        Task<CRUDReturn> GetAttendances(int studentNo);
        Task<CRUDReturn> GetAttendances(int studentNo, Remarks remarks);

        Task<CRUDReturn> GetAttendances(Remarks remarks);

        Task<CRUDReturn> GetAttendances(string room, int studentNo, Remarks remarks);

        //Get All Attendance of Students for Faculty (only assigned subject)
        Task<CRUDReturn> GetStudFacAttendance(int facultyId);

        //Verifies Student Attendance if it aligns with Subject - Room - Schedule
        Task<bool> VerifyAttendance(long rfid, string room);

        //Get All Attendance of Students per Subject
        Task<CRUDReturn> GetStudentAttendance(int studentNo);

        //After Verification add attendance
        Task<CRUDReturn> AddStudentAttendance(AddAttendanceDto attendance);

        Remarks CheckRemarks(DateTime timeIn, DateTime timeOut, SubjectSchedule sched);


    }
}
