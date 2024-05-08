using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly ModelContext _context;

        public MenuRepository(ModelContext context) : base(context)
        {
            _context = context;
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
