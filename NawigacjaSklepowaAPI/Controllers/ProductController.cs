using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NawigacjaSklepowaAPI.Attributes;
using NawigacjaSklepowaAPI.Data;
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

        
        [HttpPost("findProduct")]
        public async Task<IActionResult> FindProduct(FindingProductDto request)
        {
            var result = await _productService.FindProduct(request);

            return Ok(result);
        }

        [Authorize]
        [RequiresClaim(Identity.AppAdminUserClaimName, "true")]
        [HttpPost("createProduct")]
        public async Task<IActionResult> CreateProduct(ProductCreationDto request)
        {
            var result = await _productService.CreateProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }

        [HttpPost("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(ProductDeletionDto request)
        {
            var result = await _productService.DeleteProduct(request);
            if (!result.result)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
