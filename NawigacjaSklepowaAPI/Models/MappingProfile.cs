using AutoMapper;
using NawigacjaSklepowaAPI.Data.Entities;
using NawigacjaSklepowaAPI.Models.Auth;

namespace NawigacjaSklepowaAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserRegistrationDto>().ReverseMap();
            CreateMap<Shop, ShopCreationDto>().ReverseMap();
            CreateMap<Product, ProductCreationDto>().ReverseMap();
        }
    }
}
