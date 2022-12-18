using Samids_API.Models;

namespace Samids_API.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAttendances();

        //Get All Attendance of Students for Faculty per Subject
        Task<IEnumerable<Attendance>> GetStudFacAttendance(int subjectId, int facultyId);

        //Verifies Student Attendance if it aligns with Subject - Room - Schedule
        Task<bool> VerifyAttendance(long rfid, string room);

        //Get All Attendance of Students per Subject
        Task<IEnumerable<Attendance>> GetStudentAttendance(int subjectId, int studentId);

        //After Verification add attendance
        Task<Attendance> AddStudentAttendance(Attendance attendance);


    }
}
