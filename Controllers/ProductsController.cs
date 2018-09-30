using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Data.Repositories;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;
using ShoppingApp.WebAPI.Services;

namespace ShoppingApp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public ProductsController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await repository.GetProducts();

            var result = mapper.Map<IEnumerable<ProductResource>>(products);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = nameof(GetProduct))]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await repository.GetProduct(id);

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
            var category = await repository.GetCategory(payload.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                return BadRequest(ModelState);
            }

            var product = mapper.Map<Product>(payload);

            repository.AddProduct(product);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<ProductResource>(product);

            return CreatedAtRoute(nameof(GetProduct), new { id = product.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaveProductResource payload)
        {
            var category = await repository.GetCategory(payload.CategoryId);

            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                return BadRequest(ModelState);
            }

            var product = await repository.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            mapper.Map<SaveProductResource, Product>(payload, product);

            await unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await repository.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            repository.RemoveProduct(product);

            if (await unitOfWork.CompleteAsync() > 0)
            {
                foreach (var model in product.Models)
                {
                    foreach (var photo in model.Photos)
                    {
                        photoService.Delete(photo.FileName);
                    }
                }
            }

            return NoContent();
        }
    }
}