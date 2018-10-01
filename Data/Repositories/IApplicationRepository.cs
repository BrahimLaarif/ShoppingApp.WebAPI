using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.WebAPI.Entities.Models;

namespace ShoppingApp.WebAPI.Data.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);

        Task<IEnumerable<Color>> GetColors();
        Task<Color> GetColor(int id);

        Task<IEnumerable<Size>> GetSizes();
        Task<Size> GetSize(int id);

        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        void AddProduct(Product product);
        void RemoveProduct(Product product);

        Task<IEnumerable<Model>> GetModelsByProductId(int productId);
        Task<Model> GetModelByProductId(int productId, int id);
        Task<Model> GetModel(int id);

        Task<IEnumerable<Photo>> GetPhotos(int modelId);
        Task<Photo> GetPhoto(int modelId, int id);

        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        void AddUser(User user, string password);
        void RemoveUser(User user);

        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        void AddOrder(Order order);
        void RemoveOrder(Order order);
    }
}