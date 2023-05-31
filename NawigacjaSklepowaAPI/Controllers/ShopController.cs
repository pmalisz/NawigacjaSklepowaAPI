using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
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

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
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
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("createShop")]
        public async Task<IActionResult> CreateShop(ShopCreationDto request)
        {
            var result = await _shopService.CreateShop(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
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
