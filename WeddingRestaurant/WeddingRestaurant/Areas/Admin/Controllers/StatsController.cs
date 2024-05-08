using Microsoft.AspNetCore.Mvc;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
