using AutoMapper;
using Replate.Application.Features.FoodListingItem.Dtos;

using FoodListingItemEntity = Replate.Domain.Entities.FoodListingItem;

namespace Replate.Application.Features.FoodListingItem;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FoodListingItemEntity, FoodListingItemDto>();
        CreateMap<FoodListingItemDto, FoodListingItemEntity>();
    }
}