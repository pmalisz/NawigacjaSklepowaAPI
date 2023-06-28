using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Shops;
using NawigacjaSklepowaAPI.Services.Interfaces;

namespace NawigacjaSklepowaAPI.Services
{
    public class ShelfService : IShelfService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ShelfService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Shelf>> GetAllForShop(int shopId)
        {
            return await _context.Shelves.Where(s => s.ShopId == shopId).ToListAsync();
        }

        public async Task<(bool result, string message)> ManageShelves(ShopLayoutDto request)
        {
            Shop? shop = _context.Shops.Find(request.ShopId);
            if (shop is null)
                return (false, "Nie istnieje sklep o takim Id");

            foreach(var shelfDto in request.Shelves)
            {
                if(shelfDto.Id is null)
                {
                    Shelf createdShelf = _mapper.Map <Shelf>(shelfDto);
                    createdShelf.ShopId = request.ShopId;
                    _context.Shelves.Add(createdShelf);
                    continue;
                }

                Shelf? updatedShelf = _context.Shelves.Find(shelfDto.Id);
                if (updatedShelf is null)
                    return (false, "Nie istnieje półka o takim Id");


                updatedShelf.Name = shelfDto.Name;
                updatedShelf.Color = shelfDto.Color;
                updatedShelf.Height = shelfDto.Height;
                updatedShelf.Width = shelfDto.Width;
                updatedShelf.X = shelfDto.X;
                updatedShelf.Y = shelfDto.Y;
            }

            var shelves = _context.Shelves.Where(s => s.ShopId == request.ShopId).ToList();
            var toDelete = shelves.Where(s => !request.Shelves.Any(s2 => s2.Id == s.Id));
            _context.Shelves.RemoveRange(toDelete);

            await _context.SaveChangesAsync();
            return (true, "");
        }
    }
}
