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
    [Route("api/products/{productId}/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ModelsController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels(int productId)
        {
            var models = await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .Where(m => m.ProductId == productId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<ModelResource>>(models);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetModel))]
        public async Task<IActionResult> GetModel(int productId, int id)
        {
            var product = await context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var model = await context.Models
                .Include(m => m.Color)
                .Include(m => m.ModelSizes).ThenInclude(ms => ms.Size)
                .Include(m => m.Photos)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                return NotFound();
            }
            
            var result = mapper.Map<ModelResource>(model);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int productId, [FromBody] SaveModelResource payload)
        {
            var product = await context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            var color = await context.Colors.FindAsync(payload.ColorId);

            if (color == null)
            {
                ModelState.AddModelError("ColorId", "Invalid ColorId");
                return BadRequest(ModelState);
            }

            foreach(var sizeId in payload.Sizes)
            {
                if (await context.Sizes.FindAsync(sizeId) == null)
                {
                    ModelState.AddModelError("SizeId", "Invalid SizeId");
                    return BadRequest(ModelState);
                }
            }

            var model = mapper.Map<Model>(payload);

            product.Models.Add(model);
            await context.SaveChangesAsync();

            var result = mapper.Map<ModelResource>(model);

            return CreatedAtRoute(nameof(GetModel), new { id = model.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int productId, int id, [FromBody] SaveModelResource payload)
        {
            var product = await context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            var color = await context.Colors.FindAsync(payload.ColorId);

            if (color == null)
            {
                ModelState.AddModelError("ColorId", "Invalid ColorId");
                return BadRequest(ModelState);
            }

            foreach(var sizeId in payload.Sizes)
            {
                if (await context.Sizes.FindAsync(sizeId) == null)
                {
                    ModelState.AddModelError("SizeId", "Invalid SizeId");
                    return BadRequest(ModelState);
                }
            }

            var model = await context.Models
                .Include(m => m.ModelSizes)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            mapper.Map<SaveModelResource, Model>(payload, model);

            await context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int productId, int id)
        {
            var product = await context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var model = await context.Models.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            product.Models.Remove(model);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}