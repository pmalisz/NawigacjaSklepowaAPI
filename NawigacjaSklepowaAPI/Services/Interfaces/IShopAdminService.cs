using NawigacjaSklepowaAPI.Models.Employees;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IShopAdminService
    {
        Task<(bool result, string Message)> CreateEmployee(EmployeeCreationDto request);
    }
}
