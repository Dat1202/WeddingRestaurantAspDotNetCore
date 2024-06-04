using WeddingRestaurant.Heplers;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ModelContext db;
        public CartRepository(ModelContext context) 
        {
            db = context;
        }
        public async Task<Order> CreateOrderAsync(Order order, List<CartItem> cart, Event cartEvent)
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
                db.AddRange(cthds);

                Event e = new Event
                {
                    OrderId = order.Id,
                    Name = cartEvent.Name,
                    Time = cartEvent.Time,
                    NumberTable = cartEvent.NumberTable,
                    RoomId = cartEvent.RoomId,
                    Note = cartEvent.Note,
                };
                db.Events.Add(e);
                await db.SaveChangesAsync();

                return order;
        }
    }

}
