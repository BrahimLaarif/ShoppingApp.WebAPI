using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShoppingApp.WebAPI.Common.Utilities;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SaveModelResource
    {
        public int ColorId { get; set; }

        [EnsureOneElementAttribute(ErrorMessage = "At least one size is required")]
        public ICollection<int> Sizes { get; set; }
        
        public double Price { get; set; }

        public SaveModelResource()
        {
            Sizes = new Collection<int>();
        }
    }
}