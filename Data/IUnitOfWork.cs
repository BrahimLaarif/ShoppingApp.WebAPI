using System.Threading.Tasks;

namespace ShoppingApp.WebAPI.Data
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}