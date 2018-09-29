using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SavePhotoResource
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }
    }
}