using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool IsUserRegistered, string Message)> Register(UserRegistrationDto request);
        Task<User?> Login(UserLoginDto request);
    }
}
