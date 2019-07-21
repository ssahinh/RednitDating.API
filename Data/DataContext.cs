using Microsoft.EntityFrameworkCore;
using RednitDating.Api.Models;

namespace RednitDating.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }

}