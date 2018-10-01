using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class ItemModelResource
    {
        public int Id { get; set; }
        public ColorResource Color { get; set; }
        public ModelProductResource Product { get; set; }
        public ICollection<PhotoResource> Photos { get; set; }

        public ItemModelResource()
        {
            Photos = new Collection<PhotoResource>();
        }
    }
}