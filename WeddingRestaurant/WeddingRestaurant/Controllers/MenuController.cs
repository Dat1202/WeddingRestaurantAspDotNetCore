using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Controllers
{
    public class MenuController : Controller
    {
        private readonly ModelContext _context;

        public MenuController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var menu = await _context.TypeMenus
                .Where(tm => tm.Id == id)
                .Join(
                    _context.Menus,
                    tm => tm.Id,
                    m => m.TypeID,
                    (tm, m) => new TypeMenuVM
                    {
                        Id = m.Id,
                        Name = m.Name
                    }
                )
                .ToListAsync();

            return View(menu);
        }

    }
}
