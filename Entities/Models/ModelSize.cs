using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.WebAPI.Entities.Models
{
    [Table("ModelSizes")]
    public class ModelSize
    {
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        public int SizeId { get; set; }
        public virtual Size Size { get; set; }
    }
}