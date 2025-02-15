using Microsoft.EntityFrameworkCore;
using Ribbit_API.Models;

namespace Ribbit_API.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbOptions) : base(dbOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

        public DbSet<Product> Products { get; set; }
    }
}
