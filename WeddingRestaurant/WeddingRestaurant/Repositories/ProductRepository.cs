using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ModelContext _context;

        public ProductRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

    }
}
