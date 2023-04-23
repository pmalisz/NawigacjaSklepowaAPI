using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Data.Entities;
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
        public async Task<IActionResult> Register(Shop request)
        {
            var result = await _appAdminService.CreateShop(request);
            if(!result.result)
                return BadRequest(result.Message);
            
            return Ok();
        }

    }
}
