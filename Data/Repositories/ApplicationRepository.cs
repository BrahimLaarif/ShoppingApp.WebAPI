using System;
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

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await context.Colors.ToListAsync();
        }

        public async Task<Color> GetColor(int id)
        {
            return await context.Colors.FindAsync(id);
        }

        public async Task<IEnumerable<Size>> GetSizes()
        {
            return await context.Sizes.ToListAsync();
        }

        public async Task<Size> GetSize(int id)
        {
            return await context.Sizes.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
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

        public async Task<IEnumerable<Model>> GetModelsByProductId(int productId)
        {
            return await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .Where(m => m.ProductId == productId)
                .ToListAsync();
        }

        public async Task<Model> GetModelByProductId(int productId, int id)
        {
            return await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .Where(m => m.ProductId == productId)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Model> GetModel(int id)
        {
            return await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int modelId)
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

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for(var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void AddUser(User user, string password)
        {
            byte[] passwordSalt, passwordHash;
            CreatePasswordHash(password, out passwordSalt, out passwordHash);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            context.Users.Add(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public void RemoveUser(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await context.Orders
                .Include(o => o.User)
                .Include(o => o.Items).ThenInclude(i => i.Model)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Color)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Photos)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Product)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Product).ThenInclude(p => p.Category)
                .Include(o => o.Items).ThenInclude(i => i.Size)
                .ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await context.Orders
                .Include(o => o.User)
                .Include(o => o.Items).ThenInclude(i => i.Model)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Color)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Photos)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Product)
                .Include(o => o.Items).ThenInclude(i => i.Model).ThenInclude(m => m.Product).ThenInclude(p => p.Category)
                .Include(o => o.Items).ThenInclude(i => i.Size)
                .SingleOrDefaultAsync(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            order.Status = Status.Pending;
            order.Reference = Guid.NewGuid().GetHashCode().ToString("x").ToUpper();
            order.ItemsTotal = order.Items.Select(i => i.Amount).Sum();
            order.ShippingTotal = ShippingCost.GetCost(order.ShippingMethod);
            order.PurchaseTotal = order.ItemsTotal + order.ShippingTotal;

            context.Orders.Add(order);
        }

        public void RemoveOrder(Order order)
        {
            context.Orders.Remove(order);
        }
    }
}