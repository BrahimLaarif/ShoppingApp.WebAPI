namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class ItemResource
    {
        public int Id { get; set; }
        public ItemModelResource Model { get; set; }
        public SizeResource Size { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
    }
}