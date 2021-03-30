using Microsoft.EntityFrameworkCore;
using WebApiRest.Models;
using WebApiRest.Models.Mappings;

namespace WebApiRest.Services
{
    public class DatabaseService : DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}