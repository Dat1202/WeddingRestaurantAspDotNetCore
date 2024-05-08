using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.ViewComponents
{
    public class StatsByFilterViewComponent : ViewComponent
    {
        private ModelContext _context;
        public StatsByFilterViewComponent(ModelContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stats = await (from o in _context.Orders
                               join od in _context.OrderDetails on o.Id equals od.OrderId
                               group od by o.OrderDate.Month into g
                               select new StatsVM
                               {
                                   Month = g.Key,
                                   TotalPrice = g.Sum(od => od.Price)
                               }).ToListAsync();

            return View("Stats", stats);
        }

    }
}
