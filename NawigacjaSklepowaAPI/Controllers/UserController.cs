using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Services.Interfaces;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [RequiresClaim(Identity.AppAdminUserClaimName, "true")]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _userService.GetById(id);
            if (user is null)
                return NotFound("Nie znaleziono użytkownika");

            return Ok(user);
        }
    }
}
