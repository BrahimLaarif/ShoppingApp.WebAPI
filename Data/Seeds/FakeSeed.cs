using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Data.Seeds
{
    public class FakeSeed : ISeed
    {
        private readonly ApplicationDbContext context;

        public FakeSeed(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            CategorySeed();
            ColorSeed();
            SizeSeed();
            ProductSeed();
            UserSeed();
            OrderSeed();
        }

        private void CategorySeed()
        {
            if (!context.Categories.Any())
            {
                var categoriesJsonData = System.IO.File.ReadAllText("Data/Seeds/FakeData/Categories.json");
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoriesJsonData);

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private void ColorSeed()
        {
            if (!context.Colors.Any())
            {
                var colorsJsonData = System.IO.File.ReadAllText("Data/Seeds/FakeData/Colors.json");
                var colors = JsonConvert.DeserializeObject<IEnumerable<Color>>(colorsJsonData);

                context.Colors.AddRange(colors);
                context.SaveChanges();
            }
        }

        private void SizeSeed()
        {
            if (!context.Sizes.Any())
            {
                var sizeJsonData = System.IO.File.ReadAllText("Data/Seeds/FakeData/Sizes.json");
                var size = JsonConvert.DeserializeObject<IEnumerable<Size>>(sizeJsonData);

                context.Sizes.AddRange(size);
                context.SaveChanges();
            }
        }

        private void ProductSeed()
        {
            if (!context.Products.Any())
            {
                var productsJsonData = System.IO.File.ReadAllText("Data/Seeds/FakeData/Products.json");
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsJsonData);

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private void UserSeed()
        {
            if (!context.Users.Any())
            {
                var usersJsonData = System.IO.File.ReadAllText("Data/Seeds/FakeData/Users.json");
                var users = JsonConvert.DeserializeObject<IEnumerable<User>>(usersJsonData);

                foreach(var user in users)
                {
                    using(var hmac = new HMACSHA512())
                    {
                        user.PasswordSalt = hmac.Key;
                        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
                    }
                }

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private void OrderSeed()
        {
            if (!context.Orders.Any())
            {
                var ordersJsonData = System.IO.File.ReadAllText("Data/Seeds/FakeData/Orders.json");
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(ordersJsonData);

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}