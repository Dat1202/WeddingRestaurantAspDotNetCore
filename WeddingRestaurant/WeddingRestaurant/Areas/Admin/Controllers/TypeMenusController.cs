using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TypeMenusController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeMenusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/TypeMenus
        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 12;
            var pagedList = await _unitOfWork.TypeMenus.GetAllPagedListAsync(page, pageSize);
            return View(pagedList);
        }
       
        // GET: Admin/TypeMenus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMenu = await GetTypeMenuById(id);

            if (typeMenu == null)
            {
                return NotFound();
            }

            return View(typeMenu);
        }

        // GET: Admin/TypeMenus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TypeMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TypeMenu typeMenu)
        {
            if (ModelState.IsValid)
            {
                if (await TypeMenuExistsByName(typeMenu.Name))
                {
                    TempData["TypeMenuExists"] = "TypeMenu đã tồn tại";
                    return View(typeMenu);
                }
                await _unitOfWork.TypeMenus.AddAsync(typeMenu);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeMenu);
        }

        // GET: Admin/TypeMenus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMenu = await GetTypeMenuById(id);
            if (typeMenu == null)
            {
                return NotFound();
            }
            return View(typeMenu);
        }

        // POST: Admin/TypeMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeMenu typeMenu)
        {
            if (id != typeMenu.Id)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                var existingTypeMenu = await _unitOfWork.Rooms.GetByIdAsync(id);
                string typeMenuName = typeMenu.Name.Trim();
                if (!existingTypeMenu.Name.Equals(typeMenuName))
                {
                    if (await TypeMenuExistsByName(typeMenuName))
                    {
                        TempData["TypeMenuExists"] = "TypeMenu đã tồn tại";
                        return View(typeMenu);
                    }
                }
                existingTypeMenu.Name = typeMenu.Name;
                //await _unitOfWork.TypeMenus.UpdateAsync(typeMenu);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(typeMenu);
        }

        // GET: Admin/TypeMenus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMenu = await _unitOfWork.TypeMenus.GetByIdAsync(id);

            if (typeMenu == null)
            {
                return NotFound();
            }

            return View(typeMenu);
        }

        // POST: Admin/TypeMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeMenu = await GetTypeMenuById(id);

            await _unitOfWork.TypeMenus.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> TypeMenuExistsByName(string name)
        {
            return await _unitOfWork.TypeMenus.GetTypeMenuByName(name);
        }
        private async Task<TypeMenu> GetTypeMenuById(int id)
        {
            return await _unitOfWork.TypeMenus.GetByIdAsync(id);
        }
    }
}
