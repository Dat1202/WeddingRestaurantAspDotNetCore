using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Configuration.RoleAdmin)]
    public class RoomController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Admin/Room
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;
            var pagedList = await _unitOfWork.Rooms.GetAllPagedListAsync(page, pageSize);
            return View(pagedList);
        }

        // GET: Admin/Room/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _unitOfWork.Rooms.GetByIdAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Admin/Room/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Room room, IFormFile Image)
		{
            
            ModelState.Remove("Image");

			if (ModelState.IsValid)
            {
                string roomName = room.Name.Trim();
                if (await RoomExistsByName(roomName))
                {
                    TempData["RoomExists"] = "Tên sảnh đã tồn tại";

                    return View(room);
                }
				if (Image != null)
				{
                    room.Image = MyUtil.UploadHinh(Image, "Room", Guid.NewGuid().ToString());
				}
                await _unitOfWork.Rooms.AddAsync(room);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Admin/Room/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Admin/Room/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Location,Capacity,Description")] Room room, IFormFile Image)
        {

            if (id != room.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                var existingRoom = await _unitOfWork.Rooms.GetByIdAsync(id);
                string roomName = room.Name.Trim();

                if (!existingRoom.Name.Equals(roomName))
                {
                    if (await RoomExistsByName(roomName))
                    {
                        TempData["RoomExists"] = "Tên Sảnh đã tồn tại";
                        return View(room);
                    }
                }

                if (Image != null)
                {
                    existingRoom.Image = MyUtil.UploadHinh(Image, "Room", Guid.NewGuid().ToString());
                }

                existingRoom.Name = room.Name;
                existingRoom.Price = room.Price;
                existingRoom.Location = room.Location;
                existingRoom.Capacity = room.Capacity;
                existingRoom.Description = room.Description;

                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(room);
        }

        // GET: Admin/Room/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _unitOfWork.Rooms.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Admin/Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _unitOfWork.Rooms.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoomExistsByName(string name)
        {
            return await _unitOfWork.Rooms.GetRoomByName(name);
        }
    }
}
