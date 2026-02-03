using AutoMapper;
using Replate.Application.Features.FoodListingItems.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.FoodListingItems;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO
        CreateMap<FoodListingItem, FoodListingItemDto>()
            .ForMember(d => d.FoodListingPublicId, opt => opt.MapFrom(s => s.FoodListing.PublicId));

        // DTO to Entity
        CreateMap<CreateFoodListingItemDto, FoodListingItem>();
        CreateMap<UpdateFoodListingItemDto, FoodListingItem>();
    }
}
