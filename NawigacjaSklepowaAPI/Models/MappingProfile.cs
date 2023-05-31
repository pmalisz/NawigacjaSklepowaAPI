using AutoMapper;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;
using NawigacjaSklepowaAPI.Models.Products;
using NawigacjaSklepowaAPI.Models.Shops;

namespace NawigacjaSklepowaAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<User, AccountDeletionDto>().ReverseMap();
            CreateMap<Shop, ShopCreationDto>().ReverseMap();
            CreateMap<Product, ProductCreationDto>().ReverseMap();
            CreateMap<Product, ProductDeletionDto>().ReverseMap();
        }
    }
}
