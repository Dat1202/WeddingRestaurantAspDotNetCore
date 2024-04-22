using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using System;

namespace WeddingRestaurant.Controllers
{
    public class CartController : Controller
    {
        private readonly ModelContext db;

        public CartController(ModelContext context)
        {
            db = context;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();
        [HttpGet]
        public IActionResult Index()
        {
            return View(Cart);
        }

        [HttpPost]
        public IActionResult Index(User model)
        {
            //var customerId = int.Parse(HttpContext.User.Claims.SingleOrDefault(p => p.Type == Configuration.Claim_User_Id).Value);

            var order = new Order
            {
                UserID = 5,
                PaymentMethods = ""
            };

            db.Database.BeginTransaction();
            try
            {
                db.Database.CommitTransaction();
                db.Add(order);
                db.SaveChanges();

                var cthds = new List<OrderDetail>();
                foreach (var item in Cart)
                {
                    cthds.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        ProductId = item.ProductId,
                    });
                }
                db.AddRange(cthds);
                db.SaveChanges();

                HttpContext.Session.Set<List<CartItem>>(Configuration.CART_KEY, new List<CartItem>());

                return View("Index", Cart);
            }
            catch
            {
                db.Database.RollbackTransaction();
            }

            return View("Index", Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1, string type = "Normal")
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.ProductId == id);
            if (item == null)
            {
                var products = db.Products.SingleOrDefault(p => p.Id == id);
                if (products == null)
                {
                    TempData["Message"] = $"khong tim thay";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    ProductId = products.Id,
                    Name = products.Name,
                    Price = products.Price,
                    Quantity = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            HttpContext.Session.Set(Configuration.CART_KEY, gioHang);

            if (type == "ajax")
            {
                return Json(new
                {
                    Name = item.Name,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    SoLuong = Cart.Sum(c => c.Quantity),
                    success = true
                });
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.ProductId == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(Configuration.CART_KEY, gioHang);
                return Json(new
                {
                    success = true,
                    Name = item.Name,
                    Price = item.Price,
                    ProductId = item.ProductId
                });
            }
            
            return RedirectToAction("Index");
        }
    }
}
