using Samids_API.Models;
using System.Net.WebSockets;

namespace Samids_API.Data
{
    public class DbInitializer
    {
        
        public static void Initialize(SamidsDataContext context)
        {
            if(context.Subjects.Any() && 
               context.Students.Any() && 
               context.Faculties.Any() &&
               context.Configs.Any() &&
               context.SubjectSchedules.Any()
              ) {
                return;
            }

            //Subjects init
            var IAS = new Subject() { SubjectName = "IAS", SubjectDescription = "Information Assurance And Security" };
            var PL = new Subject() { SubjectName = "PL", SubjectDescription = "Programming Languages" };
            var Test = new Subject { SubjectName = "Test", SubjectDescription = "Test Subject" };
            var Test2 = new Subject { SubjectName = "Test2", SubjectDescription = "Test Subject 2" };

            //SubjectSchedule init
            var sched1 = new SubjectSchedule() { Day = DayOfWeek.Monday, Subject = IAS, Room = "BCL1", TimeStart = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,10, 30,0), TimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 30, 0) };
            var sched2 = new SubjectSchedule() { Day = DayOfWeek.Wednesday, Subject = IAS, Room = "BCL1", TimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0), TimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 30, 0) };
            var sched3 = new SubjectSchedule() { Day = DayOfWeek.Friday, Subject = IAS, Room = "BCL1", TimeStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0), TimeEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 30, 0) };

            var SubjectSchedules = new SubjectSchedule[]
            {
                sched1, sched2, sched3
            };

            //Students init
            var Joshua = new Student { FirstName = "Joshua", LastName = "Montero", StudentNo = 2019020654, Course = "BSCS", Rfid = 187255239165, Subjects = new List<Subject> { IAS,Test}, Year = Year.Fourth };
            var Martin = new Student { FirstName = "Martin", LastName = "Lapetaje", StudentNo = 2019022233, Course = "BSCS", Rfid = 1341296684,  Subjects = new List<Subject> { IAS,Test2},Year = Year.Fourth };
            var Gilben = new Student { FirstName = "Gilben", LastName = "Ferolino", StudentNo = 2017002181, Course = "BSCS", Rfid = 228249104167, Subjects = new List<Subject> { PL, Test}, Year = Year.Fourth };
            var James = new Student { FirstName = "James", LastName = "Gadiane", StudentNo = 2019010515, Course = "BSCS", Rfid = 2301317684,  Subjects = new List<Subject> { PL, Test2},Year = Year.Fourth };

            var Students = new Student[]
            {
                Joshua, Martin, Gilben, James
            };


            //Config init
            var config = new Config();

            //Faculty init
            var Leeroy = new Faculty { FirstName = "John Leeroy", LastName = "Gadiane", Subjects = new List<Subject> { PL} , FacultyNo = 1231231};
            var Marissa = new Faculty { FirstName = "Marissa", LastName = "Buctuanon", Subjects = new List<Subject> { Test2}, FacultyNo = 3232323};
            var Dan = new Faculty { FirstName = "Daniel", LastName = "Seldura", Subjects = new List<Subject> { Test, IAS } , FacultyNo = 935939};


            var Faculties = new Faculty[]
            {
                Leeroy, Marissa, Dan
            };

            //Devices init
            var bcl1 = new Device { Room = "BCL1" };
            //Init users
            //var TestUser = new User { FirstName = "Test", LastName = "User", Email = "test@test.com", Password = "test123456", SchoolYear = "2022-2023", Student = James, Type = Models.Type.Student };

            context.Students.AddRange(Students);
            context.SaveChanges();
            context.Faculties.AddRange(Faculties);
            context.SaveChanges();
            context.SubjectSchedules.AddRange(SubjectSchedules);
            context.SaveChanges();
            context.Configs.Add(config);
            context.SaveChanges();
            /*context.Users.Add(TestUser)*/;
            context.Devices.Add(bcl1);
            context.SaveChanges();
        }
            

    }
}

