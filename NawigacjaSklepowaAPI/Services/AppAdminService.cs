using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace NawigacjaSklepowaAPI.Services
{
    public class AppAdminService : IAppAdminService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AppAdminService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool result, string Message)> CreateShop(Shop shopRequest)
        {
            bool success;
            string message;

            (success, message) = CheckShop(shopRequest);
            if (!success)
                return (false, message);

            _context.Shops.Add(shopRequest);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        private (bool, string) CheckShop(Shop shop)
        {
            // TODO
            return (true, "");
        }

    }
}
