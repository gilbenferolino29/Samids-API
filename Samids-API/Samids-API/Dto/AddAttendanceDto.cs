namespace Samids_API.Dto
{
    public class AddAttendanceDto
    {
        public int studentNo { get; set; }
        public string room { get; set; }
        public DateTime date { get; set; }
        public DateTime actualTimeIn { get; set; }
        public DateTime actualTimeout { get; set; }
    }
}
