﻿using X.PagedList;

namespace WeddingRestaurant.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<IPagedList<T>> GetAllPagedListAsync(int page, int pageSize);
    }
}
