using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManager.API.DbContexts;
using OrderManager.API.Models;

namespace OrderManager.API.Handlers;

public static class OrderLinesHandlers
{ 

    public static async Task<Ok<IEnumerable<OrderLineDto>>> GetOrderLinesAsync(
       OrderManagerDbContext orderManagerDbContext,
       [FromServices] IMapper mapper,
       int orderId)
    {
        List<Entities.OrderLine> orderLinesEntities = await orderManagerDbContext.OrderLines
            .Include(o => o.Product)
            .Where(o => o.OrderId == orderId)
            .ToListAsync();
        return TypedResults.Ok(mapper.Map<IEnumerable<OrderLineDto>>(orderLinesEntities)); 
    }

    public static async Task<Results<NotFound, Ok<OrderLineDto>>> GetOrderLineAsync(
        OrderManagerDbContext orderManagerDbContext,
        [FromServices] IMapper mapper, 
        int orderId, 
        int orderLineId)
    {
        var orderLineEntity = await orderManagerDbContext.OrderLines
            .Include(o => o.Product)
            .Where(o => o.OrderId == orderId && o.Id == orderLineId)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (orderLineEntity == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<OrderLineDto>(orderLineEntity));
    }
}
