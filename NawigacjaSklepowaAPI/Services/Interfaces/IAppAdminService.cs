using NawigacjaSklepowaAPI.Models;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IAppAdminService
    {
        Task<(bool result, string Message)> CreateShop(ShopCreationDto request);
    }
}
