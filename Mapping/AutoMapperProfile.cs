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
            // Mapping GET request
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<Model, ModelResource>()
                .ForMember(mr => mr.Sizes, opt => opt.MapFrom(m => m.ModelSizes.Select(ms => ms.SizeId)));
            CreateMap<Color, ColorResource>();
            CreateMap<Size, SizeResource>();
            CreateMap<Photo, PhotoResource>();
            
            // Mapping POST request
            CreateMap<SaveProductResource, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveModelResource, Model>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.ModelSizes, opt => opt.Ignore())
                .AfterMap((mr, m) => {
                    // Remove unselected ModelSizes
                    var removedModelSizes = new List<ModelSize>();
                    foreach(var ms in m.ModelSizes)
                    {
                        if (!mr.Sizes.Contains(ms.SizeId))
                        {
                            removedModelSizes.Add(ms);
                        }
                    }

                    foreach(var modelSize in removedModelSizes)
                    {
                        m.ModelSizes.Remove(modelSize);
                    }

                    // Add new ModelSizes
                    foreach(var sizeId in mr.Sizes)
                    {
                        if (!m.ModelSizes.Any(ms => ms.SizeId == sizeId))
                        {
                            m.ModelSizes.Add(new ModelSize() { SizeId = sizeId });
                        }
                    }

                    /*
                    // Better solutions with Linq
                    // Remove unselected ModelSizes
                    var removedModelSizes = m.ModelSizes.Where(ms => !mr.Sizes.Contains(ms.SizeId));
                    foreach(var modelSize in removedModelSizes)
                    {
                        m.ModelSizes.Remove(modelSize);
                    }

                    // Add new ModelSizes
                    var addedModelSizes = mr.Sizes.Where(sizeId => !m.ModelSizes.Any(ms => ms.SizeId == sizeId)).Select(sizeId => new ModelSize() { SizeId = sizeId });
                    foreach(var modelSize in addedModelSizes)
                    {
                        m.ModelSizes.Add(modelSize);
                    }
                    */
                });
            CreateMap<SavePhotoResource, Photo>()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}