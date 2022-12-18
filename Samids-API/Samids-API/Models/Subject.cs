using System.Text.Json.Serialization;

namespace Samids_API.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectDescription { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
        public ICollection<Faculty>? Faculties { get; set; }
    }
}
