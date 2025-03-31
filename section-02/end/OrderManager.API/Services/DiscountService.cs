using OrderManager.API.Repositories;
using OrderManager.API.Entities;

namespace OrderManager.API.Services;

public class DiscountService(IOrderRepository orderRepository) : IDiscountService
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    /// <summary>
    /// Get the order and apply the discount.  To be used for existing orders.
    /// </summary> 
    public async Task<bool> ApplyDiscountAsync(int orderId, decimal discountPercentage)
    {
        // Retrieve the order
        var order = await _orderRepository.GetByIdAsync(orderId) 
            ?? throw new Exception("Order not found.");

        // Calculate the discount amount
        var discountAmount = order.OrderTotal * (discountPercentage / 100);

        // Apply the discount
        order.OrderTotal -= discountAmount;

        // Update the order
        await _orderRepository.UpdateAsync(order);
        return true;
    }


    /// <summary>
    /// Apply the discount to an order, and return the modified order. 
    /// </summary> 
    public Order ApplyDiscount(Order order, decimal discountPercentage)
    { 
        // Calculate the discount amount
        var discountAmount = order.OrderTotal * (discountPercentage / 100);

        // Apply the discount
        order.OrderTotal -= discountAmount;

        // Return the order  
        return order;
    }
}
