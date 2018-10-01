using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShoppingApp.WebAPI.Common.Utilities;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Entities.Resources
{
    public class AddOrderResource
    {
        public int UserId { get; set; }

        [Required]
        public ShippingMethods ShippingMethod { get; set; }
        
        [EnsureOneElementAttribute(ErrorMessage = "At least one item is required")]
        public ICollection<AddItemResource> Items { get; set; }
    }
}