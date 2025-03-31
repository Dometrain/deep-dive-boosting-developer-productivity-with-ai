using AutoMapper;
using OrderManager.API.Models;
using OrderManager.API.Entities;

namespace OrderManager.API.Profiles;

public class OrderLineProfile : Profile
{
    public OrderLineProfile()
    {
        CreateMap<OrderLine, OrderLineDto>();
        CreateMap<OrderLineForCreationDto, OrderLine>(); 
    }
}
