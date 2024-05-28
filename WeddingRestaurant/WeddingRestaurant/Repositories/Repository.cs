using Azure;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using X.PagedList;

namespace WeddingRestaurant.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ModelContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ModelContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<IPagedList<T>> GetAllPagedListAsync(int page, int pageSize)
        {
            page = page < 1 ? 1 : page; 
            var query = _dbSet.AsQueryable();
            return await query.ToPagedListAsync(page, pageSize);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            //await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            //await _context.SaveChangesAsync();
            return true;
        }
    }
}