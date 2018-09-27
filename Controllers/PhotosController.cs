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
    [Route("api/products/{productId}/models/{modelId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PhotosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int productId, int modelId)
        {
            var photos = await context.Photos
                .Where(p => p.ModelId == modelId)
                .Include(p => p.Model)
                .Where(p => p.Model.ProductId == productId)
                .ToListAsync();
            
            var result = mapper.Map<IEnumerable<PhotoResource>>(photos);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetPhoto))]
        public async Task<IActionResult> GetPhoto(int productId, int modelId, int id)
        {
            var photo = await context.Photos
                .Where(p => p.ModelId == modelId)
                .Include(p => p.Model)
                .Where(p => p.Model.ProductId == productId)
                .SingleOrDefaultAsync(p => p.Id == id);

            if (photo == null)
            {
                return NotFound();
            }

            var result = mapper.Map<PhotoResource>(photo);

            return Ok(result);
        }
    }
}