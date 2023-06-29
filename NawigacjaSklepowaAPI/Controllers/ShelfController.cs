using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Models.Shops;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllForShop(int shopId)
        {
            var shelves = await _shelfService.GetAllForShop(shopId);
            return Ok(new { shelves });
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("manageLayout")]
        public async Task<IActionResult> ManageLayout(ShopLayoutDto request)
        {
            var result = await _shelfService.ManageShelves(request);
            if (!result.result)
                return BadRequest(result.message);

            return Ok();
        }
    }
}
