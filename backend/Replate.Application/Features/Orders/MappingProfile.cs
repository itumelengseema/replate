using AutoMapper;
using Replate.Application.Features.Orders.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Orders;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO
        CreateMap<Order, OrderDto>()
            .ForMember(d => d.UserPublicId, opt => opt.MapFrom(s => s.User.PublicId))
            .ForMember(d => d.UserDisplayName, opt => opt.MapFrom(s => s.User.DisplayName))
            .ForMember(d => d.FoodListingPublicId, opt => opt.MapFrom(s => s.FoodListing.PublicId))
            .ForMember(d => d.FoodListingTitle, opt => opt.MapFrom(s => s.FoodListing.Title));

        // DTO to Entity
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
    }
}
