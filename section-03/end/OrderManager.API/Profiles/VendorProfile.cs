using AutoMapper;
using OrderManager.API.Entities;
using OrderManager.API.Models;

namespace OrderManager.API.Profiles;

public class VendorProfile : Profile
{
    public VendorProfile()
    {
        CreateMap<Vendor, VendorDto>();
    }
}
