namespace Samids_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public Student? Student { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Type Type { get; set; }

        public string SchoolYear { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
    }

    public enum Type
    {
        Student,
        Faculty,
        Admin
    }
}
