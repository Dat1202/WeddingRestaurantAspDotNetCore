using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts(int page, int pageSize);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<ProductVM>> GetProductByMenuId(int id);
        Task<bool> GetProductByName(string name);
        Task<bool> AnyProductAsync(int cateId, string name);
    }
}
    