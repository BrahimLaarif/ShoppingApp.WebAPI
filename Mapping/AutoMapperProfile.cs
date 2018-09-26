using AutoMapper;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Color, CategoryResource>();
            CreateMap<SizeType, SizeTypeResource>();
            CreateMap<Product, ProductResource>();
        }
    }
}