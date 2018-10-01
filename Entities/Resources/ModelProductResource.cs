using System;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class ModelProductResource
    {
        public int Id { get; set; }
        public CategoryResource Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}