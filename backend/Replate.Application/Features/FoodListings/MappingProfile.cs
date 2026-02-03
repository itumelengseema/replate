using AutoMapper;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.FoodListings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO
        CreateMap<FoodListing, FoodListingDto>()
            .ForMember(d => d.VendorPublicId, opt => opt.MapFrom(s => s.VendorProfile.PublicId))
            .ForMember(d => d.VendorName, opt => opt.MapFrom(s => s.VendorProfile.BusinessName));

        CreateMap<FoodListingItem, FoodListingItemDto>();

        // DTO to Entity
        CreateMap<CreateFoodListingDto, FoodListing>();
        CreateMap<CreateFoodListingItemDto, FoodListingItem>();
        CreateMap<UpdateFoodListingDto, FoodListing>();
    }
}
