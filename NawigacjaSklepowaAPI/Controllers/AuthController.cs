using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Authentication.Interfaces;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(IAuthService authService, IJwtProvider jwtProvider)
        {
            _authService = authService;
            _jwtProvider = jwtProvider;
        }


        [HttpPost("register")]
        [EnableCors("Localhost")]
        public async Task<IActionResult> Register(UserRegistrationDto request)
        {
            var result = await _authService.Register(request);
            if (!result.IsUserRegistered)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var user = await _authService.Login(request);
            if (user is null)
                return BadRequest("Błędny email lub hasło");

            string token = _jwtProvider.Generate(user);

            return Ok(new { token, user });
        }

        [Authorize]
        [HttpPost("deleteAccount")]
        public async Task<IActionResult> DeleteAccount(AccountDeletionDto request)
        {
            bool isSuccess;
            string message;
            (isSuccess, message) = await _authService.DeleteAccount(request);
            if (!isSuccess)
                return BadRequest(message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.AppAdminUserClaimName, "true")]
        [HttpPost("editUser")]
        public async Task<IActionResult> EditUser(UserEditionDto request)
        {
            var result = await _authService.EditUser(request);
            if (!result.isSuccess)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
