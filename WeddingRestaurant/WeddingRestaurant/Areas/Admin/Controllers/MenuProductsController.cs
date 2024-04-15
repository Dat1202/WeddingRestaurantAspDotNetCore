﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuProductsController : Controller
    {
        private readonly ModelContext _context;

        public MenuProductsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Admin/MenuProducts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.MenuProducts.Include(m => m.Menu).Include(m => m.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Admin/MenuProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuProduct = await _context.MenuProducts
                .Include(m => m.Menu)
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuProduct == null)
            {
                return NotFound();
            }

            return View(menuProduct);
        }

        // GET: Admin/MenuProducts/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: Admin/MenuProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,ProductId")] MenuProduct menuProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name", menuProduct.MenuId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", menuProduct.ProductId);
            return View(menuProduct);
        }

        // GET: Admin/MenuProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuProduct = await _context.MenuProducts.FindAsync(id);
            if (menuProduct == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name", menuProduct.MenuId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", menuProduct.ProductId);
            return View(menuProduct);
        }

        // POST: Admin/MenuProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId,ProductId")] MenuProduct menuProduct)
        {
            if (id != menuProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuProductExists(menuProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Name", menuProduct.MenuId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", menuProduct.ProductId);
            return View(menuProduct);
        }

        // GET: Admin/MenuProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuProduct = await _context.MenuProducts
                .Include(m => m.Menu)
                .Include(m => m.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuProduct == null)
            {
                return NotFound();
            }

            return View(menuProduct);
        }

        // POST: Admin/MenuProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuProduct = await _context.MenuProducts.FindAsync(id);
            if (menuProduct != null)
            {
                _context.MenuProducts.Remove(menuProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuProductExists(int id)
        {
            return _context.MenuProducts.Any(e => e.Id == id);
        }
    }
}
