using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShoppingApp.WebAPI.Data;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hosting;
        private readonly IUnitOfWork unitOfWork;

        public PhotoService(IConfiguration configuration, IHostingEnvironment hosting, IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.hosting = hosting;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Photo> Upload(Model model, IFormFile file)
        {
            var uploadsFolderName = configuration.GetSection("FileUpload:Folder").Value;
            var uploadsFolderPath = Path.Combine(hosting.WebRootPath, uploadsFolderName);

            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo()
            {
                ModelId = model.Id,
                FilePath = Path.Combine(uploadsFolderName, fileName),
                FileName = fileName,
                Length = file.Length
            };

            model.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return photo;
        }
    }
}