using AutoMapper;
using Replate.Application.Features.Deals.DTOs;
using Replate.Domain.Entities;


namespace Replate.Application.Features.Deals;

/// <summary>
/// AutoMapper profile for Deal/FoodListing feature mappings.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    { 
        CreateMap<FoodListing, DealDto>();
        CreateMap<CreateDealDto, FoodListing>();
        CreateMap<UpdateDealDto, FoodListing>();
    }
}