using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;
            var pagedList = await _unitOfWork.Menus.GetAllMenus(page, pageSize);
            return View(pagedList);

        }
        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _unitOfWork.Menus.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Admin/Menus/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TypeID"] = new SelectList(await _unitOfWork.TypeMenus.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TypeID")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                string menuName = menu.Name.Trim();
                if (await MenuExistsByName(menuName))
                {
                    TempData["MenuExists"] = "Tên Menu đã tồn tại";
                    ViewData["TypeID"] = new SelectList(await _unitOfWork.TypeMenus.GetAllAsync(), "Id", "Name");

                    return View(menu);
                }
                await _unitOfWork.Menus.AddAsync(menu);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeID"] = new SelectList(await _unitOfWork.TypeMenus.GetAllAsync(), "Id", "Name");
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _unitOfWork.Menus.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["TypeID"] = new SelectList(await _unitOfWork.TypeMenus.GetAllAsync(), "Id", "Name");
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeID")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingMenu = await _unitOfWork.Menus.GetByIdAsync(id);
                string menuName = menu.Name.Trim();
                if (!existingMenu.Name.Equals(menuName))
                {
                    if (await MenuExistsByName(menuName))
                    {
                        TempData["MenuExists"] = "Món ăn đã tồn tại";
                        ViewData["TypeID"] = new SelectList(await _unitOfWork.TypeMenus.GetAllAsync(), "Id", "Name");

                        return View(menu);
                    }
                }

                existingMenu.Name = menu.Name;
                existingMenu.TypeID = menu.TypeID;

                //await _unitOfWork.Menus.UpdateAsync(menu);
                await _unitOfWork.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeID"] = new SelectList(await _unitOfWork.TypeMenus.GetAllAsync(), "Id", "Name");
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _unitOfWork.Menus.GetMenuById(id);

            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _unitOfWork.Menus.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MenuExistsByName(string name)
        {
            return await _unitOfWork.Menus.GetMenuByName(name);
        }
    }
}
