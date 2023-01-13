using Samids_API.Models;

namespace Samids_API.Dto
{
    public class UserRegisterDto
    {
        public int StudentNo { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; }
        public Types  Type{ get; set; }  

        public string SchoolYear = string.Empty;


    }
}
