using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Services
{
    public interface IPhotoService
    {
        Task<Photo> Upload(Model model, IFormFile file);
        bool Delete(string fileName);
    }
}