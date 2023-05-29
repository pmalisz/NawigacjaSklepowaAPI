using NawigacjaSklepowaAPI;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Helpers.Validators;
using NawigacjaSklepowaAPI.Models;
using NawigacjaSklepowaAPI.Services;

namespace NawigacjaSklepowaTest;

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

    [Fact]
    public void AddingAndRemovingShop()
    {
        var options = GetDbOptions();
        using (var context = new DataContext(options))
        {
            var shop = new Shop
            {
                Name = "Biedra",
                Address = "Uliczna",
                PostalCode = "12-345",
                City = "Krakow",
                Country = "Polska",
                Email = "test@gmail.com",
                Phone = "123456789",
            };
            context.Shops.Add(shop);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            var shop = context.Shops.Single(s => s.Email == "test@gmail.com");
            Assert.Equal("Biedra", shop.Name);
            Assert.Equal("Uliczna", shop.Address);
            Assert.Equal("12-345", shop.PostalCode);
            Assert.Equal("Krakow", shop.City);
            Assert.Equal("Polska", shop.Country);
            Assert.Equal("123456789", shop.Phone);
            context.Shops.Remove(shop);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            Assert.Empty(context.Shops);
        }
    }

    [Fact]
    public void AddingAndRemovingProduct()
    {
        var options = GetDbOptions();
        using (var context = new DataContext(options))
        {
            var product = new Product
            {
                Name = "Mleko",
                Description = "Mleko 2%",
                Category = "Nabial",
                Price = 2.99f,
                Floor = 1,
                Shelves = "5,6,7,8",
                ShopId = 1,

            };
            context.Products.Add(product);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            var product = context.Products.Single(p => p.Name == "Mleko");
            Assert.Equal("Mleko", product.Name);
            Assert.Equal("Mleko 2%", product.Description);
            Assert.Equal(2.99f, product.Price);
            Assert.Equal(1, product.Floor);
            Assert.Equal("5,6,7,8", product.Shelves);
            Assert.Equal(1, product.ShopId);
            context.Products.Remove(product);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            Assert.Empty(context.Products);
        }
    }

    [Fact]
    public void AddingAndRemovingRole()
    {
        var options = GetDbOptions();
        using (var context = new DataContext(options))
        {
            var role = new Role
            {
                ClaimName = "Admin",
            };
            context.Roles.Add(role);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            var role = context.Roles.Single(r => r.ClaimName == "Admin");
            Assert.Equal("Admin", role.ClaimName);
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            Assert.Empty(context.Roles);
        }
    }

    [Fact]
    public void AddingAndRemovingLayout()
    {
        var options = GetDbOptions();
        using (var context = new DataContext(options))
        {
            var layout = new Layout
            {
                Canvas = "x:5,y:10,w:50,h:65",
                ShopId = 1,
            };
            context.Layouts.Add(layout);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            var layout = context.Layouts.Single(l => l.ShopId == 1);
            Assert.Equal("x:5,y:10,w:50,h:65", layout.Canvas);
            Assert.Equal(1, layout.ShopId);
            context.Layouts.Remove(layout);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            Assert.Empty(context.Layouts);
        }
    }

    [Fact]
    public void CheckPasswordStrenght()
    {
        var password = "Test1234";
        Assert.True(AuthService.CheckPassword(password, password).Item1);
        password = "Ab123456";
        Assert.True(AuthService.CheckPassword(password, password).Item1);
        password = "0123456789yZ";
        Assert.True(AuthService.CheckPassword(password, password).Item1);


        password = "Ab012345678901234567890123456789012345678901234567890123456789";
        Assert.False(AuthService.CheckPassword(password, password).Item1);
        password = "A1234567";
        Assert.False(AuthService.CheckPassword(password, password).Item1);
        password = "a1234567";
        Assert.False(AuthService.CheckPassword(password, password).Item1);
        password = "AbCdEfGh";
        Assert.False(AuthService.CheckPassword(password, password).Item1);
        password = "Ab12345";
        Assert.False(AuthService.CheckPassword(password, password).Item1);
        Assert.False(AuthService.CheckPassword("Test1234", "Test4321").Item1);
        Assert.False(AuthService.CheckPassword("Test1234", "Test12345").Item1);
    }

    [Fact]
    public void CheckEmailToBeCorrect()
    {
        Assert.True(MyValidators.CheckEmail("test@gmail.com").Item1);
        Assert.True(MyValidators.CheckEmail("test@wp.pl").Item1);
        Assert.True(MyValidators.CheckEmail("test@op.pl").Item1);

        
        Assert.False(MyValidators.CheckEmail("test").Item1);
        Assert.False(MyValidators.CheckEmail("test@").Item1);
        Assert.False(MyValidators.CheckEmail("test@.com").Item1);
        Assert.False(MyValidators.CheckEmail("test@gmail").Item1);
        Assert.False(MyValidators.CheckEmail("test@gmail.").Item1);
    }

    [Fact]
    public void CheckPostalCode()
    {
        Assert.True(MyValidators.CheckPostalCode("12-345").Item1);
        Assert.True(MyValidators.CheckPostalCode("00-000").Item1);
        
        Assert.False(MyValidators.CheckPostalCode("12345").Item1);
        Assert.False(MyValidators.CheckPostalCode("123-45").Item1);
        Assert.False(MyValidators.CheckPostalCode("12-3456").Item1);
        Assert.False(MyValidators.CheckPostalCode("12-34").Item1);
        Assert.False(MyValidators.CheckPostalCode("12-3a5").Item1);
    }

    [Fact]
    public void CheckPhoneNumber()
    {
        Assert.True(MyValidators.CheckPhoneNumber("123456789").Item1);
        
        Assert.False(MyValidators.CheckPhoneNumber("12345678").Item1);
        Assert.False(MyValidators.CheckPhoneNumber("12345678a").Item1);
        Assert.False(MyValidators.CheckPhoneNumber("48-123456789").Item1);
        Assert.False(MyValidators.CheckPhoneNumber("123-456-789").Item1);
        Assert.False(MyValidators.CheckPhoneNumber("123 456 789").Item1);
    }

    private static DbContextOptions<DataContext> GetDbOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(DbName)
            .Options;
    }
}