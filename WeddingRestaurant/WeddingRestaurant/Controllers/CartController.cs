using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;
using WeddingRestaurant.Heplers;

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
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.Id == id);
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
                    Id = products.Id,
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

            return RedirectToAction("Index");
        }
    }
}
