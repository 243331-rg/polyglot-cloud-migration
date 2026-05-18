namespace backend_dotnet.Models
{
    public class MigrationTask
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
