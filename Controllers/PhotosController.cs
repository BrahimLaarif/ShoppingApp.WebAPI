using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Data.Repositories;
using ShoppingApp.WebAPI.Entities.Models;
using ShoppingApp.WebAPI.Entities.Resources;

namespace ShoppingApp.WebAPI.Controllers
{
    [Route("api/products/{productId}/models/{modelId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PhotosController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int productId, int modelId)
        {
            var model = await repository.GetModel(productId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            var photos = await repository.GetPhotos(modelId);

            var result = mapper.Map<IEnumerable<PhotoResource>>(photos);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetPhoto))]
        public async Task<IActionResult> GetPhoto(int productId, int modelId, int id)
        {
            var model = await repository.GetModel(productId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            var photo = await repository.GetPhoto(modelId, id);

            if (photo == null)
            {
                return NotFound();
            }

            var result = mapper.Map<PhotoResource>(photo);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int productId, int modelId, [FromBody] SavePhotoResource payload)
        {
            var model = await repository.GetModel(productId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            var photo = mapper.Map<Photo>(payload);

            model.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<PhotoResource>(photo);

            return CreatedAtRoute(nameof(GetPhoto), new { id = photo.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int productId, int modelId, int id, [FromBody] SavePhotoResource payload)
        {
            var model = await repository.GetModel(productId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            var photo = await repository.GetPhoto(modelId, id);

            if (photo == null)
            {
                return NotFound();
            }

            mapper.Map<SavePhotoResource, Photo>(payload, photo);

            await unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int productId, int modelId, int id)
        {
            var model = await repository.GetModel(productId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            var photo = await repository.GetPhoto(modelId, id);

            if (photo == null)
            {
                return NotFound();
            }

            model.Photos.Remove(photo);
            await unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}