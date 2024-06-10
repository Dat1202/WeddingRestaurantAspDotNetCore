using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.ViewComponents
{
    public class MenuItemViewComponent : ViewComponent
    {
		private readonly IUnitOfWork _unitOfWork;

		public MenuItemViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id, List<int> productIdsInCart)
        {
            var products = await _unitOfWork.Products.GetProductByMenuId(id, productIdsInCart);

			return View("MenuSubItem", products);
        }

    }
}
