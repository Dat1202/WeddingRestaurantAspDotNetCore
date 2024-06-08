using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;
using X.PagedList;

namespace WeddingRestaurant.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<RoomVM> CartRoom => HttpContext.Session.Get<List<RoomVM>>(Configuration.ROOM_KEY) ?? new List<RoomVM>();
        public Event CartEvent => HttpContext.Session.Get<Event>(Configuration.EVENT_KEY) ?? new Event();

        public RoomController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? sortOrder, int[] priceRangeIds, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 6;
            var rooms = await _unitOfWork.Rooms.GetAllAsync();

            switch (sortOrder)
            {
                case "ascending":
                    ViewData["DropdownItemName"] = "Tăng dần";
                    rooms = rooms.OrderBy(r => r.Price);
                    break;
                case "decrease":
                    ViewData["DropdownItemName"] = "Giảm dần";
                    rooms = rooms.OrderByDescending(r => r.Price);
                    break;
                default:
                    ViewData["DropdownItemName"] = "Sort by";
                    rooms = rooms.OrderBy(r => r.Id); 
                    break;
            }

            var roomPagedList = await rooms.ToPagedListAsync(page, pageSize);

            ViewData["CurrentSort"] = sortOrder;

            return View(roomPagedList);
        }

        [Authorize(Roles = "User")]
		public async Task<IActionResult> CheckOut()
        {
            return View(CartRoom);
        }

		[Authorize(Roles = "User")]
		[HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutVM model, int id)
        {
            if (ModelState.IsValid)
            {
                {
                    var room = await _unitOfWork.Rooms.GetByIdAsync(id);
                    var roomPrice = room?.Price;

                    var events = new Event
                    {
                        Name = model.Name,
                        Time = model.Time,
                        NumberTable = model.NumberTable,
						RoomId= id,
                        Note = model.Note,
                    };

                    HttpContext.Session.Set(Configuration.EVENT_KEY, events);

                    return RedirectToAction("Index", "Cart", new { roomPrice = roomPrice });
                }
            }

            return View();
        }

        public async Task<IActionResult> AddToCartAsync(int id, string type = "Normal")
        {
            var gioHang = CartRoom;
            if (gioHang.Count() > 0 )
            {
                TempData["Message"] = "Chỉ có thể thêm 1 sảnh";
            }
            else
            {
                var item = gioHang.SingleOrDefault(p => p.Id == id);
                if (item == null)
                {
                    var r = await _unitOfWork.Rooms.GetByIdAsync(id);
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
                        Capacity = r.Capacity,
                        Location = r.Location,
                        Image = r.Image,
                    };
                    gioHang.Add(item);
                }

                HttpContext.Session.Set(Configuration.ROOM_KEY, gioHang);

            }
			if (type == "ajax")
			{
				return Json(new
				{
					success = true,
					message = TempData["Message"]
				});
			}
			return RedirectToAction("Index");

		}
		public IActionResult RemoveCart(int[] ids, string type = "Normal")
		{
			var gioHang = CartRoom;

			foreach (var id in ids)
			{
				var item = gioHang.SingleOrDefault(p => p.Id == id);
				if (item != null)
				{
					gioHang.Remove(item);
				}
			}

			HttpContext.Session.Set(Configuration.ROOM_KEY, gioHang);

			if (type == "ajax")
			{
				return Json(new
				{
					success = true
				});
			}

			return RedirectToAction("CheckOut");
		}
    }
}
