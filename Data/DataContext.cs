using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Data
{
    public class DataContext : DbContext
    {
        #pragma warning disable CS8618
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
