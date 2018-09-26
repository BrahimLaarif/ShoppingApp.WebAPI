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

        public virtual ICollection<Size> Sizes { get; set; }

        public Model()
        {
            Sizes = new HashSet<Size>();
        }
    }
}