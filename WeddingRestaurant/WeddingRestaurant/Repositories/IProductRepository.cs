using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
    }
}
