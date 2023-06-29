using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
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

        public async Task<List<Product>> GetAllForUser(int userId)
        {
            var users = await _context.Users.Where(u => u.Id == userId).ToListAsync();
            var shops= await _context.Shops.Where(s => s.Email == users.First().Email).ToListAsync();

            return await _context.Products.Where(p => p.ShopId == shops.First().Id).ToListAsync();
        }

            return await _context.Products.Where(p => shelves.Any(s => s.Id == p.ShelfId)).ToListAsync();
        }

        public async Task<(bool result, string Message)> CreateProduct(ProductCreationDto request)
        {
            Product product = _mapper.Map<Product>(request);

            if (!_context.Shelves.Any(s => s.Id == product.ShelfId))
            {
                return (false, "Nie ma takiej półki");
            }

            if (_context.Products.Any(
                p => p.Name.ToLower() == product.Name.ToLower() &&
                p.ShelfId == product.ShelfId)
            )
            {
                return (false, "Ten produkt już istnieje na tej półce");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool result, string Message)> UpdateProduct(ProductUpdateDto request)
        {
            Product? product = _context.Products.Find(request.Id);

            if (product is null)
                return (false, "Nie istnieje produkt o takim Id");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Category = request.Category;
            product.Price = request.Price;
            product.ShelfId = request.ShelfId;
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool result, string Message)> DeleteProduct(ProductDeletionDto request)
        {
            Product? product = _context.Products.Find(request.Id);

            if (product is null)
                return (false, "Nie istnieje produkt o takim Id");

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
