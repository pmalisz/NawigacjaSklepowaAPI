using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Models;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppAdminController : ControllerBase
    {
        private readonly IAppAdminService _appAdminService;

        public AppAdminController(IAppAdminService authService)
        {
            _appAdminService = authService;
        }

        [HttpPost("createShop")]
        public async Task<IActionResult> CreateShop(ShopCreationDto request)
        {
            var result = await _appAdminService.CreateShop(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductCreationDto request)
        {
            var result = await _appAdminService.CreateProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
