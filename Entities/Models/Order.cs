namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public double Total { get; set; }
    }
}