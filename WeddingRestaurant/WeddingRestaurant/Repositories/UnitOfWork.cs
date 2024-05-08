using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;

        public UnitOfWork(ModelContext context, IProductRepository productRepository, 
            IMenuRepository menuRepository, ITypeMenuRepository typeMenus, ICategoryRepository categories)
        {
            _context = context;
            Products = productRepository;
            Menus = menuRepository;
            TypeMenus = typeMenus;
            Categories = categories;
        }

        public IProductRepository Products { get; private set; }
        public IMenuRepository Menus { get; private set; }
        public ITypeMenuRepository TypeMenus { get; private set; }
        public ICategoryRepository Categories { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
