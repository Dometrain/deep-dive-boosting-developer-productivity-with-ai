using OrderManager.API.Entities;

namespace OrderManager.API.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<IEnumerable<Order>> GetAllOrdersAsync(bool includeOrderLines = false);
    Task<Order?> GetOrderByIdAsync(int orderId, bool includeOrderLines = false);
}
