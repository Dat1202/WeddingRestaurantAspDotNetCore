using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Interfaces
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IEnumerable<Menu>> GetAllMenus(int page, int pageSize);
        Task<IEnumerable<MenuVM>> GetMenuByTypeMenuId(int? id);
    }
}
