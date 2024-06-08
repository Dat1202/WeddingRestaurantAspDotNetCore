using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Configuration.RoleAdmin)]
    public class StatsController : Controller
    {
        private readonly ModelContext _context;
        public StatsController(ModelContext context) {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Stats(int? month, int? quarter, int? year)
        {
            IQueryable<StatsVM> query = from o in _context.Orders
                                        join od in _context.OrderDetails on o.Id equals od.OrderId
                                        where (month == null || o.OrderDate.Month == month) &&
                                              (year == null || o.OrderDate.Year == year)
                                        group od by o.OrderDate.Month into g
                                        select new StatsVM
                                        {
                                            Month = g.Key,
                                            TotalPrice = g.Sum(od => od.Price)
                                        };
            if (quarter != null)
            {
                query = query.Where(s => ((s.Month - 1) / 3) == (quarter - 1));
            }

            var stats = await query.ToListAsync();
            return Json(stats);
        }
    }
}
