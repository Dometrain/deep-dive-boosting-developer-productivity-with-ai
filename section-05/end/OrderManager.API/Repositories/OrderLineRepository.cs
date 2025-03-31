using Microsoft.EntityFrameworkCore;
using OrderManager.API.DbContexts;
using OrderManager.API.Entities;

namespace OrderManager.API.Repositories;

public class OrderLineRepository : IOrderLineRepository
{
    private readonly OrderManagerDbContext _context;

    public OrderLineRepository(OrderManagerDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderLine>> GetAllOrderLinesAsync(int orderId)
    {
        return await _context.OrderLines
                             .Where(ol => ol.OrderId == orderId)
                             .ToListAsync();
    }

    public async Task<OrderLine?> GetOrderLineByIdAsync(int orderId, int orderLineId)
    {
        return await _context.OrderLines
                             .Where(ol => ol.OrderId == orderId && ol.Id == orderLineId)
                             .FirstOrDefaultAsync();
    }

    public async Task<OrderLine> CreateOrderLineAsync(OrderLine orderLine)
    {
        await _context.OrderLines.AddAsync(orderLine); 
        return orderLine;
    }

    public Task UpdateOrderLineAsync(OrderLine orderLine)
    {
        _context.OrderLines.Update(orderLine);
        return Task.CompletedTask;
    }

    public async Task DeleteOrderLineAsync(int orderLineId)
    {
        var orderLine = await _context.OrderLines.FindAsync(orderLineId);
        if (orderLine != null)
        {
            _context.OrderLines.Remove(orderLine); 
        }
    }
}
