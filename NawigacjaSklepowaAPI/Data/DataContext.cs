using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, ClaimName = Identity.ClientUserClaimName },
                new Role { Id = 2, ClaimName = Identity.EmployeeUserClaimName },
                new Role { Id = 3, ClaimName = Identity.ManagerUserClaimName },
                new Role { Id = 4, ClaimName = Identity.ShopAdminUserClaimName },
                new Role { Id = 5, ClaimName = Identity.AppAdminUserClaimName }
            );
        }
    }
}
