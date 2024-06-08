using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<ListOrderVM>> GetOrderByUser(ApplicationUser UserId);
    }
}
