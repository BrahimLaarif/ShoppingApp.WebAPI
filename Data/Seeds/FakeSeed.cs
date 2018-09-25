using System.Collections.Generic;
using System.Linq;
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
            ProductSeed();
        }

        private void CategorySeed()
        {
            if (!context.Categories.Any())
            {
                var categoriesJsonData = System.IO.File.ReadAllText("Data/Seeds/MockData/Categories.json");
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoriesJsonData);

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private void ProductSeed()
        {
            if (!context.Products.Any())
            {
                var productsJsonData = System.IO.File.ReadAllText("Data/Seeds/MockData/Products.json");
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productsJsonData);

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}