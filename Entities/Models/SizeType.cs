using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Models
{
    public class SizeType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}