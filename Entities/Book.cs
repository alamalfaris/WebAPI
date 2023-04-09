namespace WebAPI.Entities
{
    public class Book : CommonEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string PublishedYear { get; set; } = string.Empty;
    }
}
