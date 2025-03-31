using Microsoft.EntityFrameworkCore;
using OrderManager.API.DbContexts;
using OrderManager.API.Entities;

namespace OrderManager.API.Repositories; 

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(OrderManagerDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool includeOrderLines = false)
    {
        if (includeOrderLines)
        {
            return await _context.Orders.Include(o => o.OrderLines).ToListAsync();
        }
        else
        {
            return await _context.Orders.ToListAsync();
        }
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, bool includeOrderLines = false)
    {
        if (includeOrderLines)
        {
            return await _context.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.Id == orderId);
        }
        else
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
