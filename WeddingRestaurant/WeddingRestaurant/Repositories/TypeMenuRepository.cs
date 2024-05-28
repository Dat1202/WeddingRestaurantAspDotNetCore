using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Repositories
{
    public class TypeMenuRepository : Repository<TypeMenu>, ITypeMenuRepository
    {
        private readonly ModelContext _context;

        public TypeMenuRepository(ModelContext context) : base(context)
        {
            _context = context;

        }

        public async Task<bool> GetTypeMenuByName(string name)
        {
            return await _context.TypeMenus.AnyAsync(e => e.Name.Equals(name));
        }
    }
}
