using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Controllers
{
    [Authorize(Roles = "User")]
    public class ListOrderController : Controller
    {
        private readonly ModelContext _modelContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public ListOrderController(UserManager<ApplicationUser> userManager, ModelContext modelContext)
        {
            _modelContext = modelContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user != null)
            {
                var orderId = await _modelContext.Orders
                                            .Where(u => u.UserId == user)
                                            .Select(u => u.Id)
                                            .ToListAsync();

                var danhSachDonHang = (
                    from od in _modelContext.OrderDetails
                    join p in _modelContext.Products on od.ProductId equals p.Id
                    where orderId.Contains(od.OrderId)
                    group new { od, p } by od.OrderId into grouped

                    select new ListOrderVM
                    {
                        OrderId = grouped.Key,
                        PaymentMethods = grouped.FirstOrDefault().od.Order.PaymentMethods,
                        OrderDetails = grouped.Select(g => new OrderDetailVM
                        {
                            ProductName = g.p.Name,
                            UnitPrice = g.od.Price,
                            ProductID = g.p.Id
                        }).ToList()
                    }).ToList();
                return View(danhSachDonHang);
            }

            return View();
        }
    }
}
