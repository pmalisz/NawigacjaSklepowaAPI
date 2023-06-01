using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Authentication.Interfaces;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Models.Shops;
using NawigacjaSklepowaAPI.Services;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly IUserService _userService;
        private readonly IJwtProvider _jwtProvider;

        public ShopController(IShopService shopService, IUserService userService, IJwtProvider jwtProvider)
        {
            _shopService = shopService;
            _userService = userService;
            _jwtProvider = jwtProvider;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync(int id)
        {
            var shop = await _shopService.Get(id);
            if (shop is null)
                return NotFound("Nie znaleziono sklepu");

            return Ok(shop);
        }

        [Authorize]
        [RequiresClaim(Identity.ClientUserClaimName, "true")]
        [HttpPost("createShop")]
        public async Task<IActionResult> CreateShop(ShopCreationDto request)
        {
            var result = await _shopService.CreateShop(request);
            if (!result.result)
                return BadRequest(result.Message);

            var user = _userService.UpdateRole(request.UserId, Identity.ShopAdminUserClaimName);
            string token = _jwtProvider.Generate(user.Result);

            return Ok(new { token });
        }

        [Authorize]
        [RequiresClaim(Identity.ClientUserClaimName, "true")]
        [HttpPost("rateShop")]
        public async Task<IActionResult> RateShop(RateShopDto request)
        {
            var result = await _shopService.RateShop(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
