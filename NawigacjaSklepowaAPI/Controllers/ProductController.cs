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

        [HttpGet("getAll")]
        public async Task<IActionResult> GetGetAllForShop(int shopId)
        {
            var products = await _productService.GetAllForShop(shopId);
            return Ok(new { products });
        }

        [HttpPost("findProduct")]
        public async Task<IActionResult> FindProduct(FindingProductDto request)
        {
            var result = await _productService.FindProduct(request);

            return Ok(new { result });
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
