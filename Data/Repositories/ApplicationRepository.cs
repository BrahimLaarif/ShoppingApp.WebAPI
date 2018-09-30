using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Data.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<List<Color>> GetColors()
        {
            return await context.Colors.ToListAsync();
        }

        public async Task<Color> GetColor(int id)
        {
            return await context.Colors.FindAsync(id);
        }

        public async Task<List<Size>> GetSizes()
        {
            return await context.Sizes.ToListAsync();
        }

        public async Task<Size> GetSize(int id)
        {
            return await context.Sizes.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.Models).ThenInclude(m => m.Color)
                .Include(p => p.Models).ThenInclude(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(p => p.Models).ThenInclude(m => m.Photos)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.Models).ThenInclude(m => m.Color)
                .Include(p => p.Models).ThenInclude(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(p => p.Models).ThenInclude(m => m.Photos)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            context.Products.Remove(product);
        }

        public async Task<List<Model>> GetModels(int productId)
        {
            return await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .Where(m => m.ProductId == productId)
                .ToListAsync();
        }

        public async Task<Model> GetModel(int productId, int id)
        {
            return await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .Where(m => m.ProductId == productId)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Photo>> GetPhotos(int modelId)
        {
            return await context.Photos
                .Where(p => p.ModelId == modelId)
                .ToListAsync();
        }

        public async Task<Photo> GetPhoto(int modelId, int id)
        {
            return await context.Photos
                .Where(p => p.ModelId == modelId)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public void AddUser(User user, string password)
        {
            using(var hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            context.Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            context.Users.Remove(user);
        }
    }
}