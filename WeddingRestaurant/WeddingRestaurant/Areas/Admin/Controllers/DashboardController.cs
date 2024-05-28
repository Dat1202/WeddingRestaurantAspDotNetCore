using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ModelContext _model;

        public DashboardController(ModelContext model)
        {
            _model = model;
        }
        public IActionResult Index()
        {
            DateTime currentDate = DateTime.Now;

            int orderCount = _model.Orders
                .Where(o => o.OrderDate >= currentDate.AddDays(-2))
                .Count();
            ViewBag.OrderCount = orderCount;    

            int userCount = _model.Users.Count();
            ViewBag.UserCount = userCount;

            return View();
        }
    }
}
