using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Products;

namespace NawigacjaSklepowaAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllForShop(int shopId);
        Task<List<Product>> FindProduct(FindingProductDto request);

        Task<(bool result, string Message)> CreateProduct(ProductCreationDto request);

        Task<(bool result, string Message)> DeleteProduct(ProductDeletionDto request);

        Task<(bool result, string Message)> RateProduct(RateProductDto request);
    }
}
