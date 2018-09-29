using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hosting;

        public PhotosController(IApplicationRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IHostingEnvironment hosting)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.configuration = configuration;
            this.hosting = hosting;
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

            var uploadsFolderName = configuration.GetSection("UploadsFolderName").Value;
            var uploadsFolderPath = Path.Combine(hosting.WebRootPath, uploadsFolderName);

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(payload.File.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await payload.File.CopyToAsync(stream);
            }

            var photo = new Photo()
            {
                ModelId = model.Id,
                FilePath = Path.Combine(uploadsFolderName, fileName),
                FileName = fileName,
                Length = payload.File.Length
            };

            model.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

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
            await unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}