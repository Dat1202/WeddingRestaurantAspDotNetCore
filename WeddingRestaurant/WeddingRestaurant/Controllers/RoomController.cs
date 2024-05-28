﻿using System;
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
using X.PagedList;

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
        public async Task<IActionResult> Index(string sortOrder, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 6;
            var rooms = _context.Rooms.ToPagedList(page, pageSize);

            if (sortOrder == "decrease")
            {
                rooms = rooms.OrderByDescending(r => r.Price).ToPagedList(page, pageSize);
            }   
            else if (sortOrder == "ascending")
            {
                rooms = rooms.OrderBy(r => r.Price).ToPagedList(page, pageSize);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_RoomsList", await rooms.ToListAsync());
            }

            return View(rooms);
        }
        [Authorize(Roles = "User")]
		public async Task<IActionResult> CheckOut()
        {
            return View(Cart);
        }
		[Authorize(Roles = "User")]
		[HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutVM model, int? id)
        {
            if (ModelState.IsValid)
            {
                {
					var events = new Event
                    {
                        Name = model.Name,
                        Time = model.Time,
                        NumberTable = model.NumberTable,
						RoomId= id,
                        Note = model.Note,
						UserId = await _userManager.GetUserAsync(User),
                    };
                    var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);

                    var roomPrice = room?.Price;

                    _context.Add(events);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Cart", new { numberTable = model.NumberTable, roomPrice = roomPrice });
                }
            }

            return View();
        }
        public IActionResult AddToCart(int id, string type = "Normal")
        {
            var gioHang = Cart;
            if (gioHang.Count() > 0 )
            {
                TempData["Message"] = "Chỉ có thể thêm 1 sảnh";
            }
            else
            {
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
			var gioHang = Cart;

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
