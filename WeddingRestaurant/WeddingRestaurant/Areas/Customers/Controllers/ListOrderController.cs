using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Customers.Controllers
{
    [Area("Customers")]
    [Authorize(Roles = Configuration.RoleUser)]
    public class ListOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public ListOrderController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user != null)
            {
                return View(await _unitOfWork.Orders.GetOrderByUser(user));
            }

            return View();
        }
    }
}
