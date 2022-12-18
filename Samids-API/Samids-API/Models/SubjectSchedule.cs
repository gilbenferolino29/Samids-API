using System.ComponentModel.DataAnnotations;

namespace Samids_API.Models
{
    public class SubjectSchedule
    {
        [Key]
        public int SchedId { get; set; }
        public Subject? Subject { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public Day Day { get; set; }
        public string Room { get; set; } = string.Empty;

    }
    
    public enum Day
    {
        Monday,
        Tuesday, 
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
}
