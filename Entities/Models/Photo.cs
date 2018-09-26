using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        [Required]
        public string Url { get; set; }
    }
}