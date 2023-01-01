using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Samids_API.Models
{
    public class Attendance
    {
        public int attendanceId { get; set; }
        public Student? Student { get; set; }
        public SubjectSchedule? SubjectSchedule { get; set; }
        public Device? Device { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }
        
        public DateTime? ActualTimeIn { get; set; }

        public DateTime? ActualTimeOut { get; set; }
        
        public Remarks remarks { get; set; }
    }

    public enum Remarks
    {
        OnTime, // 0
        Late, // 1
        Cutting, // 2
        Absent // 3

    }
}
