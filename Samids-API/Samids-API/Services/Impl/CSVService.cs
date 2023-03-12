using CsvHelper;
using CsvHelper.Configuration;
using Samids_API.Data;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Services.Impl
{
    public class CSVService : ICSVService
    {

        private readonly SamidsDataContext _context;

        public CSVService(SamidsDataContext context)
        {
            _context = context;
        }

        // Student
        public async Task<CRUDReturn> ReadStudentsFromCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<StudentCsvMap>();
            var students = csv.GetRecords<Student>().ToList();

            await _context.Students.AddRangeAsync(students);
            return new CRUDReturn { success = true, data = students };
        }
        public sealed class StudentCsvMap : ClassMap<Student>
        {
            public StudentCsvMap()
            {
                Map(m => m.StudentID).Ignore();
                Map(m => m.StudentNo).Name("Student Number");
                Map(m => m.Rfid).Name("RFID");
                Map(m => m.LastName).Name("Last Name");
                Map(m => m.FirstName).Name("First Name");
                Map(m => m.Course).Name("Course");
                Map(m => m.Year).Name("Year Level");
            }
        }

        // Subject
        public async Task<CRUDReturn> ReadSubjectsFromCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<SubjectCsvMap>();
            var subjects = csv.GetRecords<Subject>().ToList();

            await _context.Subjects.AddRangeAsync(subjects);
            return new CRUDReturn { success = true, data = subjects };

        }


        public sealed class SubjectCsvMap : ClassMap<Subject>
        {
            public SubjectCsvMap()
            {
                Map(m => m.SubjectID).Ignore();
                Map(m => m.SubjectName).Name("Subject Name");
                Map(m => m.SubjectDescription).Name("Subject Description");
                References<SubjectScheduleCsvMap>(m => m.SubjectSchedules);
            }
        }

        public sealed class SubjectScheduleCsvMap : ClassMap<SubjectSchedule>
        {
            public SubjectScheduleCsvMap()
            {
                Map(m => m.SchedId).Ignore();
                Map(m => m.TimeStart).Name("Start Time");
                Map(m => m.TimeEnd).Name("End Time");
                Map(m => m.Day).Name("Day");
                Map(m => m.Room).Name("Room");
            }
        }

        // Faculty
        public async Task<CRUDReturn> ReadFacultiesFromCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

            csv.Context.RegisterClassMap<FacultyCsvMap>();
            var faculties = csv.GetRecords<Faculty>().ToList();

            await _context.Faculties.AddRangeAsync(faculties);
            return new CRUDReturn { success = true, data = faculties };
        }
        public sealed class FacultyCsvMap : ClassMap<Faculty>
        {
            public FacultyCsvMap()
            {
                Map(m => m.FacultyId).Ignore();
                Map(m => m.FacultyNo).Name("Faculty Number");
                Map(m => m.LastName).Name("Last Name");
                Map(m => m.FirstName).Name("First Name");
            }
        }



    }
}
