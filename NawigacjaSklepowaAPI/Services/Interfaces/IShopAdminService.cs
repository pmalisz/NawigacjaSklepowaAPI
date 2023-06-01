using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Employees;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IShopAdminService
    {
        Task<(bool result, string Message)> CreateEmployee(EmployeeCreationDto request);
        Task<(bool result, string Message)> CreateManager(EmployeeCreationDto request);
        Task<(bool result, string Message)> DeleteEmployee(AccountDeletionDto request);
    }
}
