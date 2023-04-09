namespace WebAPI.Request.Student
{
    public class StudentRequest : CommonRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
    }
}
