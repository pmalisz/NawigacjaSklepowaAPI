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
    }
}
