using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SavePhotoResource
    {
        [Required]
        public IFormFile File { get; set; }
    }
}