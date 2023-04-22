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
            // Check if password is strong enough
            var min_chars = 8;
            var max_chars = 32;

            if (request.Password.Length < min_chars)
                return (false, $"Hasło musi mieć co najmniej {min_chars} znaków.");

            if (request.Password.Length > max_chars)
                return (false, $"Hasło może mieć maksymalnie {max_chars} znaki.");

            if (!request.Password.Any(char.IsUpper))
                return (false, "Hasło musi zawierać co najmniej jedną wielką literę.");

            if (!request.Password.Any(char.IsLower))
                return (false, "Hasło musi zawierać co najmniej jedną małą literę.");

            if (!request.Password.Any(char.IsDigit))
                return (false, "Hasło musi zawierać co najmniej jedną cyfrę.");

            // Check if passwords match
            if (request.Password != request.ConfirmPassword)
                return (false, "Hasła nie są takie same.");

            // Check if user with given email already exists
            if (_context.Users.Any(u => u.Email.ToLower() == request.Email.ToLower()))
                return (false, "Użytkownik z takim emailem już istnieje.");


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
