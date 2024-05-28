using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ModelContext _context;

        public CategoryRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GetCategoryByName(string name)
        {
            return await _context.Categories.AnyAsync(e => e.Name.Equals(name));
        }
    }
}
