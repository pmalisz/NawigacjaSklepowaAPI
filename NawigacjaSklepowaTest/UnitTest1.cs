using NawigacjaSklepowaAPI;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaTest
{
    public class UnitTest1
    {
        private const string DbName = "NawigacjaSklepowaDb";

        [Fact]
        public void AddingAndRemovingUser()
        {
            var options = GetDbOptions();

            using (var context = new DataContext(options))
            {
                var user = new User
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "test@gmail.com",
                    Password = "test",
                    RoleId = 1,
                    ShopId = 1,
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                var user = context.Users.Single(u => u.Email == "test@gmail.com");
                Assert.Equal("Jan", user.FirstName);
                Assert.Equal("Kowalski", user.LastName);
                Assert.Equal(1, user.RoleId);
                Assert.Equal(1, user.ShopId);
                context.Users.Remove(user);
                context.SaveChanges();
            }

            using (var context = new DataContext(options))
            {
                Assert.Empty(context.Users);
            }
        }
        private static DbContextOptions<DataContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(DbName)
                .Options;
        }
    }
}