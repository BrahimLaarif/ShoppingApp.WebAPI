using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShoppingApp.WebAPI.Common.Utilities;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SaveModelResource
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        
        // [EnsureOneElement(ErrorMessage = "At least one size is required")]
        // public ICollection<int> Sizes { get; set; }

        // [EnsureOneElement(ErrorMessage = "At least one photo is required")]
        // public ICollection<SavePhotoResource> Photos { get; set; }

        public double Price { get; set; }

        public SaveModelResource()
        {
            // Sizes = new Collection<int>();
            // Photos = new Collection<SavePhotoResource>();
        }
    }
}