using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Interfaces
{
    public interface ICartRepository
    {
        Task<Order> CreateOrderAsync(Order order, List<CartItem> cart, Event cartEvent);
    }
}
