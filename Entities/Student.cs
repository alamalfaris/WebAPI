namespace WebAPI.Entities
{
    public class Student : CommonEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;

    }
}
