using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Models.Shops;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IShopService
    {
        Task<List<Shop>> GetAll();
        Task<Shop?> Get(int Id);
        Task<Shop?> GetByUserId(int userId);
        Task<(bool result, string Message)> CreateShop(ShopCreationDto request);
        Task<(bool result, string Message)> RateShop(RateShopDto request);
    }
}
