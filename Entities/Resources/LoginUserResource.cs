using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class LoginUserResource
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}