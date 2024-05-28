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
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
  }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 12;
            var pagedList = await _unitOfWork.Products.GetAllProducts(page, pageSize);

            return View(pagedList);

        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,IsAvailable,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                string productName = product.Name.Trim();
                bool productExistsInCategory = await _unitOfWork.Products.AnyProductAsync(product.CategoryId, productName);

                if (productExistsInCategory)
                {
                    TempData["ProductExists"] = "Món ăn đã tồn tại trong danh mục này.";
                    ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);

                    return View(product);
                }
                if (await ProductExistsByName(productName))
                {
                    TempData["ProductExists"] = "Món ăn đã tồn tại";
                    ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);

                    return View(product);
                }
                

                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsAvailable,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
                string productName = product.Name.Trim();

                if (!existingProduct.Name.Equals(productName))
                {
                    if (await ProductExistsByName(productName))
                    {
                        TempData["ProductExists"] = "Món ăn đã tồn tại";
                        ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);

                        return View(product);
                    }
                }

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.IsAvailable = product.IsAvailable;
                existingProduct.CategoryId = product.CategoryId;
                //await _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _unitOfWork.Categories.GetAllAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _unitOfWork.Products.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> ProductExistsByName(string name)
        {
            return await _unitOfWork.Products.GetProductByName(name);
        }
        private async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.Products.GetProductById(id);
        }
    }
}
