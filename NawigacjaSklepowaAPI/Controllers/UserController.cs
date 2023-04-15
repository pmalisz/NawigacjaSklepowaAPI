using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Services;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly DataContext _dataContext;
        public UserController(IUserService userService, DataContext dataDbContext)
        {
            _userService = userService;
            _dataContext = dataDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

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
