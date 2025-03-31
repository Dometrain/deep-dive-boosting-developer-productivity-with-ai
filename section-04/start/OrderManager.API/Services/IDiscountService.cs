using OrderManager.API.Entities;

namespace OrderManager.API.Services;

public interface IDiscountService
{
    Task<bool> ApplyDiscountAsync(int orderId, decimal discountPercentage);
    Order ApplyDiscount(Order order, decimal discountPercentage);
}