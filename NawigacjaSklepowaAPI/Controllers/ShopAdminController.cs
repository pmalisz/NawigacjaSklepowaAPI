﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;
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
        public async Task<IActionResult> CreateProduct(EmployeeCreationDto request)
        {
            var result = await _shopAdminService.CreateEmployee(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}