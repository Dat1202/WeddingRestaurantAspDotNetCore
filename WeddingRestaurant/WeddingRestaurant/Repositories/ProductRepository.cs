using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Product>> GetAllAsync()
        {
            var p = await _model.Products!.ToListAsync();
            return p;
        }
    }
}
