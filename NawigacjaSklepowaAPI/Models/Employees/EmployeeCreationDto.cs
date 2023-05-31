using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;

namespace NawigacjaSklepowaAPI.Models.Employees
{
    public class EmployeeCreationDto
    {
        public required UserRegistrationDto User { get; set; }
        public required int ShopId { get; set; }
    }
}