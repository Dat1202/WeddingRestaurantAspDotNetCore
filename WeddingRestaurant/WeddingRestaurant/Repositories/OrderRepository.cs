using Microsoft.EntityFrameworkCore;
using WeddingRestaurant.Interfaces;
using WeddingRestaurant.Models;
using WeddingRestaurant.ViewModels;

namespace WeddingRestaurant.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ModelContext _context;

        public OrderRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ListOrderVM>> GetOrderByUser(ApplicationUser UserId)
        {
            var orderId = await _context.Orders.AsNoTracking()
                                            .Where(u => u.UserId == UserId)
                                            .Select(u => u.Id)
                                            .ToListAsync();

            return await 
                (from od in _context.OrderDetails
                join p in _context.Products on od.ProductId equals p.Id
                where orderId.Contains(od.OrderId)
                group new { od, p } by od.OrderId into grouped

                select new ListOrderVM
                {
                    OrderId = grouped.Key,
                    PaymentMethods = grouped.FirstOrDefault().od.Order.PaymentMethods,
                    OrderDetails = grouped.Select(g => new OrderDetailVM
                    {
                        ProductName = g.p.Name,
                        UnitPrice = g.od.Price,
                        ProductID = g.p.Id
                    }).ToList()
                }).ToListAsync();
        }

    }
}
