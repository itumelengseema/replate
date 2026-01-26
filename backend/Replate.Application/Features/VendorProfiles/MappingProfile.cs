using AutoMapper;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.VendorProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // VendorProfile mappings 
        CreateMap<VendorProfile, VendorProfileDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.VendorAddress));

        CreateMap<VendorProfileDto, VendorProfile>();

    }
}