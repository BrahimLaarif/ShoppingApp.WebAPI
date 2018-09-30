using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [Route("api/products/{productId}/models/{modelId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IApplicationRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PhotosController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoService = photoService;
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
        public async Task<IActionResult> Upload(int productId, int modelId, [FromForm] SavePhotoResource payload)
        {
            var model = await repository.GetModel(productId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            var photo = await photoService.Upload(model, payload.File);

            var result = mapper.Map<PhotoResource>(photo);

            return CreatedAtRoute(nameof(GetPhoto), new { id = photo.Id }, result);
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
            
            if (await unitOfWork.CompleteAsync() > 0)
            {
                photoService.Delete(photo.FileName);
            }

            return NoContent();
        }
    }
}