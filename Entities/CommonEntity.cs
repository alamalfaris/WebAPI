namespace WebAPI.Entities
{
    public class CommonEntity
    {
        public bool IsDeleted { get; set; } = false;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
