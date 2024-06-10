using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ModelContext _context;
        public CartRepository(ModelContext context) 
        {
            _context = context;
        }
        public async Task CreateOrderAsync(Order order, List<CartItem> cart, Event cartEvent)
        {
            try
            {
                var cthds = new List<OrderDetail>();
                foreach (var item in cart)
                {
                    cthds.Add(new OrderDetail
                    {
                        OrderId = order.Id,
                        Price = item.Price,
                        ProductId = item.Id,
                    });
                }
                _context.AddRange(cthds);

                Event e = new Event
                {
                    OrderId = order.Id,
                    Name = cartEvent.Name,
                    Time = cartEvent.Time,
                    NumberTable = cartEvent.NumberTable,
                    RoomId = cartEvent.RoomId ?? null,
                    Note = cartEvent?.Note,
                };
                _context.Events.Add(e);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

    }

}
