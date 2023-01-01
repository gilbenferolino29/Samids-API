using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Samids_API.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        [Required]
        public int StudentNo { get; set; }
        public  long Rfid { get; set; }
        public string LastName { get; set;} = string.Empty;
        public string FirstName { get; set;} = string.Empty;
        public string Course { get; set;} = string.Empty; 
        public Year Year { get; set;}
        
        public ICollection<Subject>? Subjects { get; set; }
    }
     
    public enum Year
        {
            First,
            Second,
            Third,
            Fourth
        }
}
