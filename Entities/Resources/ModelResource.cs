namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class ModelResource
    {
        public int Id { get; set; }
        public ColorResource Color { get; set; }
        public MaterialResource Material { get; set; }
    }
}