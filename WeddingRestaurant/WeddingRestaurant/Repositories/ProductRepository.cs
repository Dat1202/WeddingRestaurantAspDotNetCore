using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using X.PagedList;

namespace WeddingRestaurant.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ModelContext _context;

        public ProductRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts(int page, int pageSize)
        {
            return await _context.Products.Include(c => c.Category).ToPagedListAsync(page,  pageSize);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> AnyProductAsync(int cateId, string name)
        {
            return await _context.Products.AnyAsync(p => p.Name == name && p.CategoryId == cateId);
        }

        public async Task<IEnumerable<ProductVM>> GetProductByMenuId(int menuId, List<int> productIds)
		{
			return await(from mp in _context.MenuProducts 
						 join p in _context.Products on mp.ProductId equals p.Id
						 where mp.MenuId == menuId && p.IsAvailable == true
						 select new ProductVM
						 {
							 ProductId = p.Id,
							 ProductName = p.Name,
							 ProductPrice = p.Price,
                             ProductIds = productIds
                         }).ToListAsync();
        }

        public async Task<bool> GetProductByName(string name)
        {
            return await _context.Products.AnyAsync(e => e.Name.Equals(name));
        }
    }
}
