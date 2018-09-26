using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public ICollection<Model> Models { get; set; }

        public Product()
        {
            Created = DateTime.Now;
            Models = new HashSet<Model>();
        }
    }
}