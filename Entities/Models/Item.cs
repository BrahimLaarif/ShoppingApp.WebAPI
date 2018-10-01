namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}