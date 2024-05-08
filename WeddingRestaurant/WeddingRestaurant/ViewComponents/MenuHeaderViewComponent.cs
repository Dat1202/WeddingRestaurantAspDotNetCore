using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.Repositories;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.ViewComponents
{
    public class MenuHeaderViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuHeaderViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var typeMenus = await _unitOfWork.TypeMenus.GetAllAsync();

            return View("MenuHeaderItem", typeMenus.Select(m => new TypeMenuVM
            {
                Id = m.Id,
                Name = m.Name,
            }));
        }
    }
}
