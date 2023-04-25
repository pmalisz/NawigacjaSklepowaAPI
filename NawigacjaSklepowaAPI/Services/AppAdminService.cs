using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Services.Interfaces;
using NawigacjaSklepowaAPI.Helpers.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            bool postalCodeOk;
            string message;
            (postalCodeOk, message) = MyValidators.checkPostalCode(shop.PostalCode);
            if (!postalCodeOk)
            {
                return (false, message);
            }
            if (!_context.Companies.Any(u => u.Id == shop.CompanyId))
            {
                return (false, "Firma o danym id nie istnieje.");
            }

            return (true, "");
        }

        public async Task<(bool result, string Message)> CreateCompany(Company companyRequest)
        {
            bool success;
            string message;

            (success, message) = CheckCompany(companyRequest);
            if (!success)
                return (false, message);

            _context.Companies.Add(companyRequest);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        private (bool, string) CheckCompany(Company company)
        {
            bool phoneOk;
            string message;
            (phoneOk, message) = MyValidators.checkPhoneNumber(company.Phone);
            if (!phoneOk)
            {
                return (false, message);
            }
            bool postalCodeOk;
            (postalCodeOk, message) = MyValidators.checkPostalCode(company.PostalCode);
            if (!postalCodeOk)
            {
                return (false, message);
            }
            if(!(new EmailAddressAttribute().IsValid(company.Email)))
            {
                return (false, "Niepoprawny adres email.");
            }
            if (_context.Companies.Any(u => u.Email.ToLower() == company.Email.ToLower()))
            {
                return (false, "Firma z takim emailem już istnieje.");
            }
                

            return (true, "");
        }

    }
}
