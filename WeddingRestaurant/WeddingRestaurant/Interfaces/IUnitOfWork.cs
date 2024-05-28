﻿namespace WeddingRestaurant.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IMenuRepository Menus { get; }
        ITypeMenuRepository TypeMenus { get; }
        ICategoryRepository Categories { get; }
        IRoomRepository Rooms { get; }
        Task<int> SaveChangesAsync();
    }
}
