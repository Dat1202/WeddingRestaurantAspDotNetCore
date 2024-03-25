using Microsoft.AspNetCore.Mvc;
using WebApplication1.Heplers;
using WebApplication1.ViewModels;

namespace WebApplication1.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var countCartItem = HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                Quantity = countCartItem.Sum(x => x.Quantity),
                TotalAmount = countCartItem.Sum(x => x.TotalPrice),
            });
        }
    }
}
