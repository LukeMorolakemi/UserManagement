using Microsoft.EntityFrameworkCore;
using UserManagement.Model;

namespace UserManagement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
       public DbSet<User> Users { get; set; }   
        public DbSet<User1>User1s { get; set; }
    }
}
