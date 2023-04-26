using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Authentication.Interfaces;
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


        //TODO: przerobić rejestracje tak, żeby była możliwa też rejestracja sklepu
        [HttpPost("register")]
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

            return Ok(token);
        }
    }
}
