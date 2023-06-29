using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Shops;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IShelfService
    {
        Task<List<Shelf>> GetAllForShop(int shopId);

        Task<(bool result, string message)> ManageShelves(ShopLayoutDto request);
    }
}
