using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Controllers
{
    public class RoomController : Controller
    {
        private readonly ModelContext _context; 
        private readonly UserManager<ApplicationUser> _userManager;

        public List<RoomVM> Cart => HttpContext.Session.Get<List<RoomVM>>(Configuration.ROOM_KEY) ?? new List<RoomVM>();

        public RoomController(ModelContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index( )
        {
            var rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }
        [Authorize(Roles = "User")]
		public async Task<IActionResult> CheckOut()
        {
            return View(Cart);
        }
		[Authorize(Roles = "User")]
		[HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutVM model, int id)
        {
            if (ModelState.IsValid)
            {
                {
					var events = new Event
                    {
                        Name = model.Name,
                        Time = model.Time,
                        NumberTable = model.NumberTable,
						RoomId= null,
						UserId = await _userManager.GetUserAsync(User),
                    };

					var r = await _context.Rooms.FirstOrDefaultAsync(a => a.Id == id);
					if (r != null)
					{
                        events.RoomId = r.Id;
					}
					_context.Add(events);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Cart", new { NumberTable = model.NumberTable });
                }
            }

            return View();
        }
        public IActionResult AddToCart(int id, string type = "Normal")
        {
            var gioHang = Cart;

                var item = gioHang.SingleOrDefault(p => p.Id == id);
                if (item == null)
                {
                    var r = _context.Rooms.SingleOrDefault(p => p.Id == id);
                    if (r == null)
                    {
                        TempData["Message"] = $"Không tìm thấy sản phẩm";
                        return Redirect("/404");
                    }
                    item = new RoomVM
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Price = r.Price,
						Capacity=r.Capacity,
						Location=r.Location,
                        Image = r.Image,
					};
                    gioHang.Add(item);
            }

            HttpContext.Session.Set(Configuration.ROOM_KEY, gioHang);

            if (type == "ajax")
            {
                return Json(new
                {
                    success = true
                });
            }

            return RedirectToAction("Index");
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
