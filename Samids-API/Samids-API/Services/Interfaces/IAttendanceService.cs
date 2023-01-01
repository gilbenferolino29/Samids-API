﻿using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAttendances();
        Task<IEnumerable<Attendance>> GetAttendances(string room);
        Task<IEnumerable<Attendance>> GetAttendances(int studentId);
        Task<IEnumerable<Attendance>> GetAttendances(Remarks remarks);
        //Get All Attendance of Students for Faculty (only assigned subject)
        Task<IEnumerable<Attendance>> GetStudFacAttendance(int facultyId);

        //Verifies Student Attendance if it aligns with Subject - Room - Schedule
        Task<bool> VerifyAttendance(long rfid, string room);

        //Get All Attendance of Students per Subject
        Task<IEnumerable<Attendance>> GetStudentAttendance(int studentId);

        //After Verification add attendance
        Task<Attendance> AddStudentAttendance(AddAttendanceDto attendance);

        Remarks CheckRemarks(DateTime timeIn, DateTime timeOut, SubjectSchedule sched);


    }
}