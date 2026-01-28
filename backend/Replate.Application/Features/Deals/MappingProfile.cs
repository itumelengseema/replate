using AutoMapper;
using Replate.Application.Features.Deals.DTOs;
using Replate.Domain.Entities;


namespace Replate.Application.Features.Deals;

/// <summary>
/// AutoMapper profile for Deal feature mappings.
/// TODO: Add mappings when DTOs are created.
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    { 
        
          CreateMap<Deal , DealDto>();
          CreateMap<CreateDealDto, Deal>();
          CreateMap<UpdateDealDto, Deal>();
  
    }
}