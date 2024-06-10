using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();

            List<int> productIdsInCart = cart.Select(item => item.Id).ToList();
            var menus = await _unitOfWork.Menus.GetMenuByTypeMenuId(id, productIdsInCart);

            return View(menus);
        }

    }
}
