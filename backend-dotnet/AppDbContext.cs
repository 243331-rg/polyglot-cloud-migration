using Microsoft.EntityFrameworkCore;

namespace backend_dotnet
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // MigrationTask ke agay 'Models.' laga dein taake direct path mil jaye
        public DbSet<backend_dotnet.Models.MigrationTask> MigrationTasks { get; set; }
    }
}