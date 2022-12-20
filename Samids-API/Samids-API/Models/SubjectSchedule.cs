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
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public DayOfWeek Day { get; set; }
        public string Room { get; set; } = string.Empty;

    }
    

}
