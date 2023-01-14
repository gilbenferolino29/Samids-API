namespace Samids_API.Dto
{
    public class AddStudentSubjectDto<T>
    {
        public int StudentNo { get; set; }
        public T? Subject { get; set; }
    }
}
