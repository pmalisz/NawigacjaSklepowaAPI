using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool IsUserRegistered, string Message)> Register(UserRegistrationDto request) 
        {
            if (_context.Users.Any(u => u.Email.ToLower() == request.Email.ToLower()))
                return (false, "Użytkownik z takim emailem już istnieje");

            User user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<User?> Login(UserLoginDto request)
        {
            var user = await _context.Users.Where(u => u.Email == request.Email).SingleOrDefaultAsync();
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            return user;
        }
    }
}
