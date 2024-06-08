namespace WeddingRestaurant.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IMenuRepository Menus { get; }
        ITypeMenuRepository TypeMenus { get; }
        ICategoryRepository Categories { get; }
        IRoomRepository Rooms { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
        IChatRepository Chats { get; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> SaveChangesAsync();
    }
}
