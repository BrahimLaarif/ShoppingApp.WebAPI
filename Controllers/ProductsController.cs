using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await context.Products
                .Include(p => p.Category)
                .Include(p => p.Models).ThenInclude(m => m.Color)
                .Include(p => p.Models).ThenInclude(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(p => p.Models).ThenInclude(m => m.Photos)
                .ToListAsync();
            
            var result = mapper.Map<IEnumerable<ProductResource>>(products);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetProduct))]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await context.Products
                .Include(p => p.Category)
                .Include(p => p.Models).ThenInclude(m => m.Color)
                .Include(p => p.Models).ThenInclude(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(p => p.Models).ThenInclude(m => m.Photos)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ProductResource>(product);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveProductResource payload)
        {
            var category = await context.Categories.FindAsync(payload.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                return BadRequest(ModelState);
            }

            var product = mapper.Map<Product>(payload);

            context.Products.Add(product);
            await context.SaveChangesAsync();

            var result = mapper.Map<ProductResource>(product);

            return CreatedAtRoute(nameof(GetProduct), new { id = product.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveProductResource payload)
        {
            var category = await context.Categories.FindAsync(payload.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                return BadRequest(ModelState);
            }

            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            mapper.Map<SaveProductResource, Product>(payload, product);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}