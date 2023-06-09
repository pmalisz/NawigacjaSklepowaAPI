﻿using AutoMapper;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Employees;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Models.Shelves;
using NawigacjaSklepowaAPI.Models.Shops;

namespace NawigacjaSklepowaAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<User, AccountDeletionDto>().ReverseMap();
            CreateMap<User, UserEditionDto>().ReverseMap();
            CreateMap<Shop, ShopCreationDto>().ReverseMap();
            CreateMap<Product, ProductCreationDto>().ReverseMap();
            CreateMap<Product, ProductDeletionDto>().ReverseMap();
            CreateMap<Employee, EmployeeCreationDto>().ReverseMap();
            CreateMap<Shelf, ShelfDto>().ReverseMap();
        }
    }
}
