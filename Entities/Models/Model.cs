using System.Collections.Generic;

namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Model
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

        public virtual ICollection<ModelSize> ModelSizes { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public double Price { get; set; }
        
        public Model()
        {
            ModelSizes = new HashSet<ModelSize>();
            Photos = new HashSet<Photo>();
        }
    }
}