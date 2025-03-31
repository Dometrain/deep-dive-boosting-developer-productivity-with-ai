using AutoMapper;
using OrderManager.API.Models;
using OrderManager.API.Entities;

namespace OrderManager.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderWithOrderLinesForCreationDto, Order>();
    }
}
