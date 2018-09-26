using System.Collections.Generic;
using AutoMapper;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;
using System.Linq;

namespace ShoppingApp.WebAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Color, ColorResource>();
            CreateMap<Size, SizeResource>();
            CreateMap<Model, ModelResource>()
                .ForMember(mr => mr.Sizes, opt => opt.MapFrom(m => m.ModelSizes.Select(ms => ms.Size)));
            
            CreateMap<Product, ProductResource>()
                .ForMember(pr => pr.Colors, opt => opt.MapFrom(p => p.Models.Select(m => m.Color).GroupBy(c => c.Id).Select(c => c.First())));
        }
    }
}