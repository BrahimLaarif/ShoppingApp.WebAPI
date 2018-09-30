using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebAPI.Entities.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public DateTime Created { get; set; }

        public User()
        {
            Created = DateTime.Now;
        }
    }
}