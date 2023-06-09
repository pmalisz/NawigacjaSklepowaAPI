﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Helpers.Validators;
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
            bool success;
            string message;

            (success, message) = CheckPassword(request.Password);
            if (!success)
                return (false, message);

            (success, message) = CheckEmail(request.Email);
            if (!success)
                return (false, message);

            User user = _mapper.Map<User>(request);
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.RoleId = _context.Roles.Single(r => r.ClaimName == Identity.ClientUserClaimName).Id;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<User?> Login(UserLoginDto request)
        {
            var user = await _context.Users.Where(u => u.Email == request.Email).Include(u => u.Role).SingleOrDefaultAsync();
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            return user;
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAccount(AccountDeletionDto request)
        {
            User? user = _context.Users.Find(request.Id);

            if (user is null)
            {
                return (false, "Nie istnieje użytkownik o takim Id");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool isSuccess, string Message)> EditUser(UserEditionDto request)
        {
            bool success;
            string message;

            (success, message) = CheckEmail(request.Email);
            if (!success)
                return (false, message);

            Role? roleId = _context.Roles.Find(request.RoleId);

            if(roleId is null)
            {
                return (false, "Rola o takim Id nie istnieje.");
            }

            User? user = _context.Users.Find(request.Id);

            if (user is null)
            {
                return (false, "Użytkownik o takim Id nie istnieje.");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.RoleId = request.RoleId;

            await _context.SaveChangesAsync();
            return (true, "");
        }

        public static (bool, string) CheckPassword(string Password)
        {
            // Check if password is strong enough
            var min_chars = 8;
            var max_chars = 32;

            if (Password.Length < min_chars)
                return (false, $"Hasło musi mieć co najmniej {min_chars} znaków.");

            if (Password.Length > max_chars)
                return (false, $"Hasło może mieć maksymalnie {max_chars} znaki.");

            if (!Password.Any(char.IsUpper))
                return (false, "Hasło musi zawierać co najmniej jedną wielką literę.");

            if (!Password.Any(char.IsLower))
                return (false, "Hasło musi zawierać co najmniej jedną małą literę.");

            if (!Password.Any(char.IsDigit))
                return (false, "Hasło musi zawierać co najmniej jedną cyfrę.");

            return (true, "");
        }

        private (bool, string) CheckEmail(string Email)
        {
            bool isValid;
            string message;

            // Check if email is valid
            (isValid, message) = MyValidators.CheckEmail(Email);
            if (!isValid)
                return (false, message);

            // Check if user with given email already exists
            if (_context.Users.Any(u => u.Email.ToLower() == Email.ToLower()))
                return (false, "Użytkownik z takim emailem już istnieje.");

            return (true, "");
        }
    }
}
