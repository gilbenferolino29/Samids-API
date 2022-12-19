namespace Samids_API.Dto
{
    public class AddAttendanceDto
    {
        public int studentId;
        public string room;
        public DateTime date;
        public DateTime actualTimeIn;
        public DateTime actualTimeout;
    }
}
