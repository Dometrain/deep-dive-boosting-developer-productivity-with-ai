using OrderManager.API.Handlers;
using OrderManager.API.Entities;

namespace OrderManager.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void RegisterOrdersEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var ordersEndpoints = endpointRouteBuilder.MapGroup("/orders");
        ordersEndpoints.MapGet("", OrdersHandlers.GetOrdersAsync);
        ordersEndpoints.MapPost("", OrdersHandlers.CreateOrderAsync);
        var orderEndpoints = ordersEndpoints.MapGroup("/{orderId:int}");
        orderEndpoints.MapGet("", OrdersHandlers.GetOrderAsync).WithName("GetOrder");
        var orderLinesEndpoints = orderEndpoints.MapGroup("/orderlines");
        orderLinesEndpoints.MapGet("", OrderLinesHandlers.GetOrderLinesAsync);
        var orderLineEndpoints = orderLinesEndpoints.MapGroup("/{orderLineId:int}");
        orderLineEndpoints.MapGet("", OrderLinesHandlers.GetOrderLineAsync);
    }

    public static void RegisterProductsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var productsEndpoints = endpointRouteBuilder.MapGroup("/products");
        productsEndpoints.MapGroup("/{productId:int}");
    }

    public static void RegisterVendorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var vendorsEndpoints = endpointRouteBuilder.MapGroup("/vendors");
        vendorsEndpoints.MapGroup("/{vendorId:int}");
    }
}
