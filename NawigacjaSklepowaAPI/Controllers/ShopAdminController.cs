using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Employees;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Services;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopAdminController : ControllerBase
    {
        private readonly IShopAdminService _shopAdminService;

        public ShopAdminController(IShopAdminService shopAdminService)
        {
            _shopAdminService = shopAdminService;
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreationDto request)
        {
            var result = await _shopAdminService.CreateEmployee(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("createManager")]
        public async Task<IActionResult> CreateManager(EmployeeCreationDto request)
        {
            var result = await _shopAdminService.CreateManager(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("deleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(AccountDeletionDto request)
        {
            var result = await _shopAdminService.DeleteEmployee(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpGet("employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _shopAdminService.GetAllEmployees();
            if (employees is null)
                return BadRequest("Nie znaleziono pracowników");

            return Ok(new { employees });
        }
    }
}
