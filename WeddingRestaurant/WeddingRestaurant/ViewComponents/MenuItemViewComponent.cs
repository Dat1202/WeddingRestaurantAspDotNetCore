using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.ViewComponents
{
    public class MenuItemViewComponent : ViewComponent
    {
        private ModelContext _context;
        public MenuItemViewComponent(ModelContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var totalPrice = await (from m in _context.Menus
                                    join mp in _context.MenuProducts on m.Id equals mp.MenuId
                                    join p in _context.Products on mp.ProductId equals p.Id
                                    where m.Id == id
                                    select p.Price).SumAsync();

            var obj = await (from m in _context.Menus
                             join mp in _context.MenuProducts on m.Id equals mp.MenuId
                             join p in _context.Products on mp.ProductId equals p.Id
                             where m.Id == id
                             select new MenuVM
                             {
                                 ProductId = p.Id,
                                 ProductName = p.Name,
                                 ProductPrice = p.Price,
                                 TotalPrice = totalPrice,
                             }).ToListAsync();

            return View("MenuSubItem", obj);
        }

    }
}
