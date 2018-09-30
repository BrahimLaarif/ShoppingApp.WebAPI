using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class LoginUserResource
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}