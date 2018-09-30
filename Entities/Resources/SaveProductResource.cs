using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ShoppingApp.WebAPI.Common.Utilities;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SaveProductResource
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}