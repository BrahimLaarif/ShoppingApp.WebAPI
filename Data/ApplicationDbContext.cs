using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<SizeType> SizeTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}