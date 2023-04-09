namespace WebAPI.Request.Book
{
    public class BookRequest : CommonRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string PublishedYear { get; set; } = string.Empty;
    }
}
