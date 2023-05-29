using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;
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

        [Authorize]
        [RequiresClaim(Identity.AppAdminUserClaimName, "true")]
        [HttpPost("createShop")]
        public async Task<IActionResult> CreateShop(ShopCreationDto request)
        {
            var result = await _appAdminService.CreateShop(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
