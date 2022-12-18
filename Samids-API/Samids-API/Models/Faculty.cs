using System.Text.Json.Serialization;

namespace Samids_API.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Subject>? Subjects{ get; set; }

    }
}
