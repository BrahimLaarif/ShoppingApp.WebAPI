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
        public string FilePath { get; set; }

        [Required]
        public string FileName { get; set; }

        public long Length { get; set; }
    }
}