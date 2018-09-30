using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Data.Repositories
{
    public interface IApplicationRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);

        Task<List<Color>> GetColors();
        Task<Color> GetColor(int id);

        Task<List<Size>> GetSizes();
        Task<Size> GetSize(int id);

        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        void AddProduct(Product product);
        void RemoveProduct(Product product);

        Task<List<Model>> GetModels(int productId);
        Task<Model> GetModel(int productId, int id);

        Task<List<Photo>> GetPhotos(int modelId);
        Task<Photo> GetPhoto(int modelId, int id);

        Task<List<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        void AddUser(User user, string password);
        void RemoveUser(User user);
    }
}