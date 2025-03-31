using System.Net;
using System.Net.Http.Json;
using Xunit;
using OrderManager.API.Models;

namespace OrderManager.API.EndToEndTests;

public class OrdersTests : IClassFixture<OrdersTestsFixture>
{
    private readonly OrdersTestsFixture _fixture;
    private readonly HttpClient _client;

    public OrdersTests(OrdersTestsFixture fixture)
    {
        _fixture = fixture;
        _client = _fixture.Factory.CreateClient();
    }

    [Fact]
    public async Task GetOrders_ReturnsOrdersList()
    {
        // Act
        var response = await _client.GetAsync("/orders");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        Assert.NotNull(orders);
        Assert.NotEmpty(orders);
    }
}
