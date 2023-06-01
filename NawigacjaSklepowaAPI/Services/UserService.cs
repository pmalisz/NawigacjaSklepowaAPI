using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> UpdateRole(int userId, string role)
        {
            var user = _context.Users.Single(u => u.Id == userId);
            user.RoleId = _context.Roles.Single(r => r.ClaimName == role).Id;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> IsEmployee(int userId)
        {
            return await _context.Employees.AnyAsync(u => u.UserId == userId);
        }
    }
}
