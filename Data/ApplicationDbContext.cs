using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}