using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly ModelContext _context;
        public OrdersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var danhSachDonHang = (
                from o in _context.Orders
                join od in _context.OrderDetails on o.Id equals od.OrderId
                join p in _context.Products on od.ProductId equals p.Id
                where od.OrderId == id
                group new { o, od, p } by o.Id into grouped

                select new ListOrderVM
                {
                    OrderId = grouped.Key,
                    PaymentMethods = grouped.FirstOrDefault().o.PaymentMethods,
                    OrderDetails = grouped.Select(g => new OrderDetailVM
                    {
                        ProductName = g.p.Name,
                        UnitPrice = g.od.Price,
                        ProductID = g.p.Id
                    }).ToList()
                }).ToList();
            return View(danhSachDonHang);
        }
    }
}
