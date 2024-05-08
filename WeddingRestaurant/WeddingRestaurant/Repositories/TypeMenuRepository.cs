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
    }
}
