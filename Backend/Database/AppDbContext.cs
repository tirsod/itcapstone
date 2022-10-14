using ItcapstoneBackend.Domain;
using Microsoft.EntityFrameworkCore;

namespace ItcapstoneBackend.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        
        private readonly string _connectionString;
        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}