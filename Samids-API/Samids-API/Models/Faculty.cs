using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Samids_API.Models
{
    public class Faculty
    {

        
        public int FacultyId { get; set; }
        [Required]
        public int FacultyNo { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Subject>? Subjects{ get; set; }

    }
}
