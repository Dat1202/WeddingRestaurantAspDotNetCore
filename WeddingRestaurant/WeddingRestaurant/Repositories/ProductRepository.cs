using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ModelContext _model;

        public ProductRepository(ModelContext model)
        {
            _model = model;
        }
        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
