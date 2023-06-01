using NawigacjaSklepowaAPI.Data.Entities;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User> UpdateRole(int id, string role);
        Task<bool> IsEmployee(int userId);
    }
}
