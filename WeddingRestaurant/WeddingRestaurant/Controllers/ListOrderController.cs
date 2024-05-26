using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            var danhSachDonHang = (
                from o in _modelContext.Orders
                join od in _modelContext.OrderDetails on o.Id equals od.OrderId
                join p in _modelContext.Products on od.ProductId equals p.Id
                where o.UserId == user
                group new { o, od, p } by o.Id into grouped

                select new ListOrderVM
                {
                    OrderId = grouped.Key,
                    PaymentMethods = grouped.FirstOrDefault().o.PaymentMethods,
                    OrderDetails = grouped.Select(g => new OrderDetailVM
                    {
                        ProductName = g.p.Name,
                        UnitPrice = g.od.Price ,
                        ProductID = g.p.Id
                    }).Take(2).ToList()
                }).ToList();
            return View(danhSachDonHang);
        }

        //public async Task<IActionResult> ListOrderDetails(int id)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var danhSachDonHang = (
        //       from o in _modelContext.Orders
        //       join od in _modelContext.OrderDetails on o.Id equals od.OrderId
        //       join p in _modelContext.Products on od.ProductId equals p.Id
        //       where o.UserId == user && od.OrderId == id
        //       group new { o, od, p } by o.Id into grouped
        //    return View();

        //}
    }
}
