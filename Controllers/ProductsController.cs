using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
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
    }
}