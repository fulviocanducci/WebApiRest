using Microsoft.EntityFrameworkCore;
using WebApiRest.Models;

namespace WebApiRest.Services
{
    public class DatabaseService: DbContext
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {            
        }

        public DbSet<Todo> Todo { get; set; }
    }
}