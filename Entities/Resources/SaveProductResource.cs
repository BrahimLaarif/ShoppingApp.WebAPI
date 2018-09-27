using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ShoppingApp.WebAPI.Common.Utilities;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class SaveProductResource
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [EnsureOneElement(ErrorMessage = "At least one model is required")]
        public ICollection<SaveModelResource> Models { get; set; }
        
        public SaveProductResource()
        {
            Models = new Collection<SaveModelResource>();
        }
    }
}