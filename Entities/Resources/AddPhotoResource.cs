using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class AddPhotoResource
    {
        [Required]
        public IFormFile File { get; set; }
    }
}