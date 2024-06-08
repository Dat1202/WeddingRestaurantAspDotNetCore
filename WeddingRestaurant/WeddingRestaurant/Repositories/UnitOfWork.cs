using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ModelContext _context;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(ModelContext context, IProductRepository productRepository, 
            IMenuRepository menuRepository, ITypeMenuRepository typeMenus, ICategoryRepository categories,
            IRoomRepository rooms, ICartRepository carts, IOrderRepository orders, IChatRepository chats)
        {
            _context = context;
            Products = productRepository;
            Menus = menuRepository;
            TypeMenus = typeMenus;
            Categories = categories;
            Rooms = rooms;
            Carts = carts;
            Orders = orders;
            Chats = chats;
        }

        public IProductRepository Products { get; private set; }
        public IMenuRepository Menus { get; private set; }
        public ITypeMenuRepository TypeMenus { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public ICartRepository Carts { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IChatRepository Chats { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            var save = await _context.SaveChangesAsync();
            return save;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void BeginTransaction()
        {
            _currentTransaction = _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
            _currentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            _currentTransaction.Rollback();
        }
    }

}
