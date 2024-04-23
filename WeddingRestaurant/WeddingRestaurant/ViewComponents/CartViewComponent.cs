using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var countCartItem = HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                TotalAmount = (double)countCartItem.Sum(x => x.TotalPrice),
            });
        }
    }
}
