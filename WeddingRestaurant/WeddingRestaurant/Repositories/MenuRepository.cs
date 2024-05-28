using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using X.PagedList;

namespace WeddingRestaurant.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly ModelContext _context;

        public MenuRepository(ModelContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Menu>> GetAllMenus(int page, int pageSize)
        {
            return await _context.Menus.Include(c => c.TypeMenu).ToPagedListAsync(page, pageSize);
        }
        public async Task<Menu> GetMenuById(int id)
        {
            return await _context.Menus.Include(tm => tm.TypeMenu).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> GetMenuByName(string name)
        {
            return await _context.Menus.AnyAsync(e => e.Name.Equals(name));
        }

        public async Task<IEnumerable<MenuVM>> GetMenuByTypeMenuId(int? id)
        {
            var menuViewModels = await _context.TypeMenus
                .Where(tm => tm.Id == id)
                .Join(
                    _context.Menus,
                    tm => tm.Id,
                    m => m.TypeID,
                    (tm, m) => new MenuVM
                    {
                        Id = m.Id,
                        Name = m.Name
                    }
                )
                .ToListAsync();

            return menuViewModels;
        }
    }
}
