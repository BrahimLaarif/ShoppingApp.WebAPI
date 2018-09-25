using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}