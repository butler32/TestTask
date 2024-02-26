using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            var maxValue = await _context.Orders.MaxAsync(p => p.Price);
            var orderWithMaxPrice = await _context.Orders.FirstOrDefaultAsync(order => order.Price == maxValue);

            return orderWithMaxPrice;
        }

        public Task<List<Order>> GetOrders()
        {
            return _context.Orders.Where(q => q.Quantity > 10).ToListAsync();
        }
    }
}
