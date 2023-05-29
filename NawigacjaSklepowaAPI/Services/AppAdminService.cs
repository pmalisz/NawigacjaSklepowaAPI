using AutoMapper;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Helpers.Validators;
using NawigacjaSklepowaAPI.Models;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Services
{
    public class AppAdminService : IAppAdminService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AppAdminService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            return (true, "");
        }

        private static (bool, string) CheckShop(ShopCreationDto shop)
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
    }
}
