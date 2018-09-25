using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data.Models;

namespace ShoppingApp.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}