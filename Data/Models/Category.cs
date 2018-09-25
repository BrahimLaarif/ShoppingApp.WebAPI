using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}