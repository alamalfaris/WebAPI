namespace WebAPI.Responses
{
    public class SearchResponse<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalDataInPage { get; set; }
        public int TotalData { get; set; }
        public int TotalPage { get; set; }
        public List<T>? Data { get; set; }
    }
}
