using Samids_API.Models;

namespace Samids_API.Dto
{
    public class StudentUpdateDto
    {
        public int StudentNo { get; set; }  
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Course { get; set; } = string.Empty;
        public Year year { get; set; }

    }
}
