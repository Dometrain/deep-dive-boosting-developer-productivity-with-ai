using OrderManager.API.Entities;

namespace OrderManager.API.Repositories;

public interface IOrderLineRepository
{
    Task<IEnumerable<OrderLine>> GetAllOrderLinesAsync(int orderId);
    Task<OrderLine?> GetOrderLineByIdAsync(int orderId, int orderLineId);
    Task<OrderLine> CreateOrderLineAsync(OrderLine orderLine);
    Task UpdateOrderLineAsync(OrderLine orderLine);
    Task DeleteOrderLineAsync(int orderLineId);
}
