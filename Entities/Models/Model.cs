namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Model
    {
        public int Id { get; set; }

        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        
        public int MaterialId { get; set; }
        public Material Material { get; set; }
    }
}