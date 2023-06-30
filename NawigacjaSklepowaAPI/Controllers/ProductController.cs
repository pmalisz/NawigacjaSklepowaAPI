using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Services;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getByUserId")]
        public async Task<IActionResult> GetAllForUser(int userId)
        {
            var products = await _productService.GetAllForUser(userId);
            return Ok(new { products });
        }

        [HttpGet("getByShopId")]
        public async Task<IActionResult> GetByShopId(int shopId)
        {
            var products = await _productService.GetAllForUser(shopId);
            return Ok(new { products });
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductCreationDto request)
        {
            var result = await _productService.CreateProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("updateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto request)
        {
            var result = await _productService.UpdateProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.ShopAdminUserClaimName, "true")]
        [HttpPost("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(ProductDeletionDto request)
        {
            var result = await _productService.DeleteProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [Authorize]
        [RequiresClaim(Identity.ClientUserClaimName, "true")]
        [HttpPost("rateProduct")]
        public async Task<IActionResult> RateProduct(RateProductDto request)
        {
            var result = await _productService.RateProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
