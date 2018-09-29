using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SavePhotoResource
    {
        public int Id { get; set; }

        [Required]
        public IFormFile File { get; set; }
    }
}