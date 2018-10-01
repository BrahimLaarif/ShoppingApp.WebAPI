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
    [Route("api/products/{productId}/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public ModelsController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetModels(int productId)
        {
            var product = await repository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var models = await repository.GetModelsByProductId(productId);

            var result = mapper.Map<IEnumerable<ModelResource>>(models);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetModel))]
        public async Task<IActionResult> GetModel(int productId, int id)
        {
            var product = await repository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var model = await repository.GetModelByProductId(productId, id);

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
            var product = await repository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var color = await repository.GetColor(payload.ColorId);

            if (color == null)
            {
                ModelState.AddModelError("ColorId", "Invalid ColorId");
                return BadRequest(ModelState);
            }

            foreach (var sizeId in payload.Sizes)
            {
                if (await repository.GetSize(sizeId) == null)
                {
                    ModelState.AddModelError("SizeId", "Invalid SizeId");
                    return BadRequest(ModelState);
                }
            }

            var model = mapper.Map<Model>(payload);

            product.Models.Add(model);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<ModelResource>(model);

            return CreatedAtRoute(nameof(GetModel), new { id = model.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int productId, int id, [FromBody] SaveModelResource payload)
        {
            var product = await repository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var color = await repository.GetColor(payload.ColorId);

            if (color == null)
            {
                ModelState.AddModelError("ColorId", "Invalid ColorId");
                return BadRequest(ModelState);
            }

            foreach (var sizeId in payload.Sizes)
            {
                if (await repository.GetSize(sizeId) == null)
                {
                    ModelState.AddModelError("SizeId", "Invalid SizeId");
                    return BadRequest(ModelState);
                }
            }

            var model = await repository.GetModelByProductId(productId, id);

            if (model == null)
            {
                return NotFound();
            }

            mapper.Map<SaveModelResource, Model>(payload, model);

            await unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int productId, int id)
        {
            var product = await repository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var model = await repository.GetModelByProductId(productId, id);

            if (model == null)
            {
                return NotFound();
            }

            product.Models.Remove(model);

            if (await unitOfWork.CompleteAsync() > 0)
            {
                foreach (var photo in model.Photos)
                {
                    photoService.Delete(photo.FileName);
                }
            }

            return NoContent();
        }
    }
}