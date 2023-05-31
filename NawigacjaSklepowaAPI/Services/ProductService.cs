using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Helpers.Validators;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Product>> GetAllForShop(int shopId)
        {
            return await _context.Products.Where(p => p.ShopId == shopId).ToListAsync();
        }

        public async Task<List<Product>> FindProduct(FindingProductDto request)
        {
            return await _context.Products.Where(product => product.Id == request.Id && product.ShopId == request.ShopId).ToListAsync();
        }

        public async Task<(bool result, string Message)> CreateProduct(ProductCreationDto productRequest)
        {
            Product product = _mapper.Map<Product>(productRequest);

            if (!_context.Shops.Any(s => s.Id == product.ShopId))
            {
                return (false, "Nie ma takiego sklepu");
            }

            if (_context.Products.Any(
                p => p.Name.ToLower() == product.Name.ToLower() &&
                p.ShopId == product.ShopId)
            )
            {
                return (false, "Ten produkt już istnieje w tym sklepie");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool result, string Message)> DeleteProduct(ProductDeletionDto request)
        {
            Product? product = _context.Products.Find(request.Id);

            if (product is null)
            {
                return (false, "Nie istnieje produkt o takim Id");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool result, string Message)> RateProduct(RateProductDto request)
        {
            Product? product = _context.Products.Find(request.Id);

            if (product is null)
                return (false, "Nie istnieje produkt o takim Id");

            product.RatingCount += 1;
            product.Rating = request.Rating;
            await _context.SaveChangesAsync();

            return (true, "");
        }
    }
}
