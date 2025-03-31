using AutoMapper;
using OrderManager.API.Entities;
using OrderManager.API.Models;

namespace OrderManager.API.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}
