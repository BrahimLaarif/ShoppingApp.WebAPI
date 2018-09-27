using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class ModelResource
    {
        public int Id { get; set; }
        public ColorResource Color { get; set; }
        public ICollection<SizeResource> Sizes { get; set; }
        public ICollection<PhotoResource> Photos { get; set; }
        public double Price { get; set; }

        public ModelResource()
        {
            Sizes = new Collection<SizeResource>();
            Photos = new Collection<PhotoResource>();
        }
    }
}