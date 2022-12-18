using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Samids_API.Models
{
    public class SubjectSchedule
    {
        [Key]
        public int SchedId { get; set; }
        [JsonIgnore]
        public Subject? Subject { get; set; }
        [Column(TypeName = "Time")]
        public TimeOnly TimeStart { get; set; }
        [Column(TypeName = "Time")]
        public TimeOnly TimeEnd { get; set; }
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
