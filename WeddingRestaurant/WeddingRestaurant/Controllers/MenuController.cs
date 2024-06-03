﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(Configuration.CART_KEY) ?? new List<CartItem>();

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var menus = await _unitOfWork.Menus.GetMenuByTypeMenuId(id);
            return View(menus);
        }

    }
}
