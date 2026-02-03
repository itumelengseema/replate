using AutoMapper;

using Replate.Application.Features.FoodListings.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.FoodListings;

/// <summary>
/// AutoMapper profile for Deal/FoodListing feature mappings.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    { 
        CreateMap<FoodListing, FoodListingDto>();
        CreateMap<CreateFoodListingDto, FoodListing>();
        CreateMap<UpdateFoodListingDto, FoodListing>();
    }
}