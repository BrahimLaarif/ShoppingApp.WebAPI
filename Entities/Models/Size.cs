namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Size
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        public int SizeTypeId { get; set; }
        public SizeType SizeType { get; set; }
        
        public double Price { get; set; }
    }
}