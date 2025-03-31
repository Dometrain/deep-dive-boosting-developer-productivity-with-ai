using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderManager.API.Entities;
using OrderManager.API.Models;
using OrderManager.API.Repositories;
using OrderManager.API.Services;
using OrderManager.API.UnitsOfWork;

namespace OrderManager.API.Handlers;


public static class OrdersHandlers
{
     
    
    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <param name="orderRepository">The order repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="includeOrderLines">Flag indicating whether to include order lines.</param>
    /// <returns>The collection of order DTOs.</returns>
    public static async Task<Ok<IEnumerable<OrderDto>>> GetOrdersAsync(
        [FromServices] IOrderRepository orderRepository,
        [FromServices] IMapper mapper,
        bool includeOrderLines = false)
    {
        // Use the repository to get the orders
        var orderEntities = await orderRepository.GetAllOrdersAsync(includeOrderLines);

        // Map the entities to DTOs
        var orderDtos = mapper.Map<IEnumerable<OrderDto>>(orderEntities);

        // Return the mapped DTOs
        return TypedResults.Ok(orderDtos);
    }

    /// <summary>
    /// Retrieves a single order by ID.
    /// </summary> 
    /// <param name="orderRepository">The order repository.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="orderId">The ID of the order to retrieve.</param>
    /// <returns>The order DTO if found, otherwise a not found result.</returns>
    public static async Task<Results<NotFound, Ok<OrderDto>>> GetOrderAsync( 
        [FromServices] IGenericRepository<Order> orderRepository,
        [FromServices] IMapper mapper,
        int orderId)
    {
        var orderEntity = await orderRepository.GetByIdAsync(orderId);

        if (orderEntity == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mapper.Map<OrderDto>(orderEntity));
    }

    /// <summary>
    /// Creates a new order with order lines.
    /// </summary> 
    /// <param name="mapper">The mapper.</param>
    /// <param name="unitOfWork">The unit of work for creating the order.</param>
    /// <param name="orderWithOrderLinesForCreationDto">The DTO containing the order and order lines data.</param>
    /// <returns>The created order DTO.</returns>
    public static async Task<CreatedAtRoute<OrderDto>> CreateOrderAsync( 
        [FromServices] IMapper mapper,
        [FromServices] CreateOrderWithOrderLinesUnitOfWork unitOfWork,
        [FromServices] IDiscountService discountService,
        OrderWithOrderLinesForCreationDto orderWithOrderLinesForCreationDto)
    {
        // Map the incoming DTO to the Order entity
        var orderEntity = mapper.Map<Order>(orderWithOrderLinesForCreationDto);

        // Extract and map order lines from the DTO
        var orderLines = mapper.Map<IEnumerable<OrderLine>>(
            orderWithOrderLinesForCreationDto.OrderLines);

        // Use the UnitOfWork to create the order and its order lines
        await unitOfWork.CreateOrderWithOrderLinesAsync(orderEntity, orderLines);

        // Apply 10% discount
        orderEntity = discountService.ApplyDiscount(orderEntity, 10);

        // Save changes
        await unitOfWork.SaveChangesAsync();

        // Map the created order entity back to a DTO to return
        var orderToReturn = mapper.Map<OrderDto>(orderEntity);

        // Return the created order
        return TypedResults.CreatedAtRoute(
            orderToReturn,
            "GetOrder",
            new { orderId = orderToReturn.Id });
    } 
}
