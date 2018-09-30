using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class UpdateUserResource
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}