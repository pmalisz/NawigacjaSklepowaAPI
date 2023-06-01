using AutoMapper;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Helpers.Validators;
using NawigacjaSklepowaAPI.Services.Interfaces;
using NawigacjaSklepowaAPI.Models.Shops;
using NawigacjaSklepowaAPI.Models.Products;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NawigacjaSklepowaAPI.Services
{
    public class ShopService : IShopService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShopService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Shop>> GetAll()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<Shop?> Get(int Id)
        {
            return await _context.Shops.FindAsync(Id);
        }

        public async Task<Shop?> GetByUserId(int userId)
        {
            var employee =  await _context.Employees.Include(e => e.Shop).SingleOrDefaultAsync(x => x.UserId == userId);

            return employee?.Shop;
        }

        public async Task<(bool result, string Message)> CreateShop(ShopCreationDto shopRequest)
        {
            bool success;
            string message;

            (success, message) = CheckShop(shopRequest);
            if (!success)
                return (false, message);

            if (_context.Shops.Any(
                s => s.Name.ToLower() == shopRequest.Name.ToLower() &&
                s.Address.ToLower() == shopRequest.Address.ToLower() &&
                s.City.ToLower() == shopRequest.City.ToLower())
            )
            {
                return (false, "Ten sklep już istnieje");
            }

            Shop shop = _mapper.Map<Shop>(shopRequest);

            _context.Shops.Add(shop);

            await _context.SaveChangesAsync();

            var employee = new Employee()
            {
                ShopId = shop.Id,
                UserId = shopRequest.UserId
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return (true, "");
        }

        private (bool, string) CheckShop(ShopCreationDto shop)
        {
            bool isValid;
            string message;

            (isValid, message) = MyValidators.CheckPostalCode(shop.PostalCode);
            if (!isValid)
                return (false, message);

            (isValid, message) = MyValidators.CheckEmail(shop.Email);
            if (!isValid)
                return (false, message);

            (isValid, message) = MyValidators.CheckPhoneNumber(shop.Phone);
            if (!isValid)
                return (false, message);

            return (true, "");
        }

        public async Task<(bool result, string Message)> RateShop(RateShopDto request)
        {
            Shop? shop = _context.Shops.Find(request.Id);

            if (shop is null)
                return (false, "Nie istnieje sklep o takim Id");

            shop.RatingCount += 1;
            shop.Rating = request.Rating;
            await _context.SaveChangesAsync();

            return (true, "");
        }
    }
}
