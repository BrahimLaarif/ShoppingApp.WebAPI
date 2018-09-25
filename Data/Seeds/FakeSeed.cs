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
            if (!context.Categories.Any())
            {
                var categoriesJsonData = System.IO.File.ReadAllText("Data/Seeds/MockData/Categories.json");
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoriesJsonData);

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }
    }
}