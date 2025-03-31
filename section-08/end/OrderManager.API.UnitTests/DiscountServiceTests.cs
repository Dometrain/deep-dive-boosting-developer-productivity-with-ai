using Moq;
using OrderManager.API.Entities;
using OrderManager.API.Repositories;
using OrderManager.API.Services;

namespace OrderManager.API.UnitTests;

public class DiscountServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly DiscountService _discountService;

    public DiscountServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _discountService = new DiscountService(_orderRepositoryMock.Object);
    }

    [Theory]
    [InlineData(1, 10, 100, 90)]
    [InlineData(1, 0, 100, 100)]
    [InlineData(1, 100, 100, 0)]
    [InlineData(1, 50, 200, 100)]
    public async Task ApplyDiscountAsync_ValidOrder_DiscountApplied(int orderId, decimal discountPercentage, decimal initialTotal, decimal expectedTotal)
    {
        // Arrange
        var order = new Order { Id = orderId, Title = "Test Order", OrderTotal = initialTotal };

        _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act
        var result = await _discountService.ApplyDiscountAsync(orderId, discountPercentage);

        // Assert
        Assert.True(result);
        Assert.Equal(expectedTotal, order.OrderTotal);
        _orderRepositoryMock.Verify(repo => repo.UpdateAsync(order), Times.Once);
    }

    [Fact]
    public async Task ApplyDiscountAsync_OrderDoesNotExist_ThrowsException()
    {
        // Arrange
        var orderId = 1;
        var discountPercentage = 10m;

        _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId))
            .ReturnsAsync((Order?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _discountService.ApplyDiscountAsync(orderId, discountPercentage));
    }

    [Theory]
    [InlineData(100, 10, 90)]
    [InlineData(100, 0, 100)]
    [InlineData(100, 100, 0)]
    [InlineData(200, 50, 100)]
    public void ApplyDiscount_ValidOrder_DiscountApplied(decimal initialTotal, decimal discountPercentage, decimal expectedTotal)
    {
        // Arrange
        var order = new Order { Title = "Test Order", OrderTotal = initialTotal };

        // Act
        var result = _discountService.ApplyDiscount(order, discountPercentage);

        // Assert
        Assert.Equal(expectedTotal, result.OrderTotal);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(110)]
    public async Task ApplyDiscountAsync_InvalidDiscountPercentage_ThrowsArgumentOutOfRangeException(decimal discountPercentage)
    {
        // Arrange
        var orderId = 1;
        var order = new Order { Id = orderId, Title = "Test Order", OrderTotal = 100m };

        _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _discountService.ApplyDiscountAsync(orderId, discountPercentage));
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(110)]
    public void ApplyDiscount_InvalidDiscountPercentage_ThrowsArgumentOutOfRangeException(decimal discountPercentage)
    {
        // Arrange
        var order = new Order { Title = "Test Order", OrderTotal = 100m };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _discountService.ApplyDiscount(order, discountPercentage));
    }

    [Theory]
    [InlineData(1, 10, 100, 90)]
    [InlineData(1, 0, 100, 100)]
    [InlineData(1, 100, 100, 0)]
    [InlineData(1, 50, 200, 100)]
    public void ApplyDiscount_ValidOrder_DiscountApplied_EdgeCases(int orderId, decimal discountPercentage, decimal initialTotal, decimal expectedTotal)
    {
        // Arrange
        var order = new Order { Id = orderId, Title = "Test Order", OrderTotal = initialTotal };

        // Act
        var result = _discountService.ApplyDiscount(order, discountPercentage);

        // Assert
        Assert.Equal(expectedTotal, result.OrderTotal);
    }

    [Theory]
    [InlineData(1, -10)]
    [InlineData(1, 110)]
    public async Task ApplyDiscountAsync_InvalidDiscountPercentage_EdgeCases(int orderId, decimal discountPercentage)
    {
        // Arrange
        var order = new Order { Id = orderId, Title = "Test Order", OrderTotal = 100m };

        _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId))
            .ReturnsAsync(order);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _discountService.ApplyDiscountAsync(orderId, discountPercentage));
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(110)]
    public void ApplyDiscount_InvalidDiscountPercentage_EdgeCases(decimal discountPercentage)
    {
        // Arrange
        var order = new Order { Title = "Test Order", OrderTotal = 100m };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _discountService.ApplyDiscount(order, discountPercentage));
    }
}