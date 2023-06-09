﻿using AutoMapper;
using NawigacjaSklepowaAPI.Data;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Employees;
using NawigacjaSklepowaAPI.Services.Interfaces;
using NawigacjaSklepowaAPI.Helpers.Validators;
using NawigacjaSklepowaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace NawigacjaSklepowaAPI.Services
{
    public class ShopAdminService : IShopAdminService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShopAdminService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool result, string Message)> CreateEmployee(EmployeeCreationDto request)
        {
            bool success;
            string message;

            (success, message) = MyValidators.CheckPassword(request.User.Password);
            if (!success)
                return (false, message);

            (success, message) = MyValidators.CheckEmail(request.User.Email);
            if (!success)
                return (false, message);

            Shop? shop = _context.Shops.Find(request.ShopId);

            if (shop == null)
            {
                return (false, "Nie ma takiego sklepu.");
            }

            Employee employee = _mapper.Map<Employee>(request);
            employee.User.Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password);

            employee.User.RoleId = _context.Roles.Single(r => r.ClaimName == Identity.EmployeeUserClaimName).Id;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool result, string Message)> CreateManager(EmployeeCreationDto request)
        {
            bool success;
            string message;

            (success, message) = MyValidators.CheckPassword(request.User.Password);
            if (!success)
                return (false, message);

            (success, message) = MyValidators.CheckEmail(request.User.Email);
            if (!success)
                return (false, message);

            Shop? shop = _context.Shops.Find(request.ShopId);

            if (shop == null)
            {
                return (false, "Nie ma takiego sklepu.");
            }

            Employee employee = _mapper.Map<Employee>(request);
            employee.User.Password = BCrypt.Net.BCrypt.HashPassword(request.User.Password);

            employee.User.RoleId = _context.Roles.Single(r => r.ClaimName == Identity.ManagerUserClaimName).Id;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<(bool result, string Message)> DeleteEmployee(AccountDeletionDto request)
        {
            Employee? emp = _context.Employees.Find(request.Id);

            if (emp is null)
            {
                return (false, "Nie istnieje pracownik o takim Id");
            }

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        public async Task<List<EmployeeSimple>> GetAllEmployees()
        {
            var employees = await _context.Users.Join(_context.Employees,
                                             u => u.Id,
                                             e => e.UserId,
                                             (u, e) => new EmployeeSimple()
                                             {
                                                 UserId = u.Id,
                                                 FirstName = u.FirstName,
                                                 LastName = u.LastName,
                                                 RoleId = u.RoleId,
                                                 ShopId = e.ShopId
                                             }).ToListAsync();

            return employees.Where(e => e.RoleId == (int)AccountType.Employee).ToList();
        }
    }
}
