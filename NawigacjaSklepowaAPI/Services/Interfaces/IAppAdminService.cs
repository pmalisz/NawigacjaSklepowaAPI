using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IAppAdminService
    {
        Task<(bool result, string Message)> CreateShop(Shop request);
        Task<(bool result, string Message)> CreateCompany(Company request);
    }
}
