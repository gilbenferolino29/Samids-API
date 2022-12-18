using System.ComponentModel.DataAnnotations;

namespace Samids_API.Models
{
    [Keyless]
    public class Config
    {
        public string CurrentTerm { get; set; } = "First Semester";
        public string CurrentYear { get; set; } = "2022-2023";
        public int LateMinutes { get; set; } = 15;
        public int AbsentMinutes { get; set; } = 30;

    }
}
