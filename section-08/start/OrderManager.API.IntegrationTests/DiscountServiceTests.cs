using Microsoft.Extensions.DependencyInjection;
using OrderManager.API.DbContexts;
using OrderManager.API.Entities;
using OrderManager.API.Repositories;
using OrderManager.API.Services;
using Xunit;

namespace OrderManager.API.IntegrationTests;

public class DiscountServiceTests : IClassFixture<OrderManagerDatabaseFixture>
{
    private readonly OrderManagerDatabaseFixture _fixture;

    public DiscountServiceTests(OrderManagerDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ApplyDiscountAsync_ShouldApplyDiscountToOrder()
    {
        // Arrange
        _fixture.ResetDatabase();
        var scope = _fixture.ServiceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OrderManagerDbContext>();
        var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var discountService = scope.ServiceProvider.GetRequiredService<IDiscountService>();

        var order = new Order
        {
            Title = "Test Order",
            OrderDate = DateTime.UtcNow,
            OrderTotal = 100m,
            OrderLines = new List<OrderLine>()
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync();

        // Act
        var result = await discountService.ApplyDiscountAsync(order.Id, 10m);

        // Assert
        var updatedOrder = await orderRepository.GetOrderByIdAsync(order.Id);
        Assert.True(result);
        Assert.NotNull(updatedOrder);
        Assert.Equal(90m, updatedOrder?.OrderTotal);
    }
}
