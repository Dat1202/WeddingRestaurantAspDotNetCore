using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.ViewComponents
{
    public class MenuHeaderViewComponent : ViewComponent
    {
        private ModelContext _context;
        public MenuHeaderViewComponent(ModelContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var typeMenus = await _context.TypeMenu.ToListAsync();

            return View("MenuHeaderItem", typeMenus.Select(m => new TypeMenuVM
            {
                Id = m.Id,
                Name = m.Name,
            }));
        }
    }
}
