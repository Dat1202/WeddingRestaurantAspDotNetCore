using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
