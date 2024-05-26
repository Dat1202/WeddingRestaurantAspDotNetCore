using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;

        public UnitOfWork(ModelContext context, IProductRepository productRepository, 
            IMenuRepository menuRepository, ITypeMenuRepository typeMenus, ICategoryRepository categories,
            IRoomRepository rooms)
        {
            _context = context;
            Products = productRepository;
            Menus = menuRepository;
            TypeMenus = typeMenus;
            Categories = categories;
            Rooms = rooms;
        }

        public IProductRepository Products { get; private set; }
        public IMenuRepository Menus { get; private set; }
        public ITypeMenuRepository TypeMenus { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public async Task<int> SaveChangesAsync()
        {
            var save = await _context.SaveChangesAsync();
            return save;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
