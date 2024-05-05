using System;
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
    public class TypeMenusController : Controller
    {
        private readonly ModelContext _context;

        public TypeMenusController(ModelContext context)
        {
            _context = context;
        }

        // GET: Admin/TypeMenus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeMenus.ToListAsync());
        }

        // GET: Admin/TypeMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMenu = await _context.TypeMenus
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(typeMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeMenu);
        }

        // GET: Admin/TypeMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMenu = await _context.TypeMenus.FindAsync(id);
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
                try
                {
                    _context.Update(typeMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeMenuExists(typeMenu.Id))
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
            return View(typeMenu);
        }

        // GET: Admin/TypeMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeMenu = await _context.TypeMenus
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var typeMenu = await _context.TypeMenus.FindAsync(id);
            if (typeMenu != null)
            {
                _context.TypeMenus.Remove(typeMenu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeMenuExists(int id)
        {
            return _context.TypeMenus.Any(e => e.Id == id);
        }
    }
}
