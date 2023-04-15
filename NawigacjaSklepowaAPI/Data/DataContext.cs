using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
