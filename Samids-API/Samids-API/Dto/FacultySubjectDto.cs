namespace Samids_API.Dto
{
    public class FacultySubjectDto<T>
    {
        public int FacultyNo { get; set; }
        public T Subject { get; set; }
    }
}
