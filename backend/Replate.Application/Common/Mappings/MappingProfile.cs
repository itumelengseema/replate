using AutoMapper;
using Replate.Application.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // VendorProfile mapping
        CreateMap<VendorProfile, VendorProfileDto>()
            .ForMember(
                dest => dest.Description, opt => opt.MapFrom(src => src.Decscription));
                

        CreateMap<VendorProfile, VendorProfileDto>();
    }
}