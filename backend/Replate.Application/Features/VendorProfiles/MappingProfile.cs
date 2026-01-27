using AutoMapper;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Domain.Entities;

namespace Replate.Application.Features.VendorProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity to DTO
        CreateMap<VendorProfile, VendorProfileDto>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.VendorAddress));
            
        CreateMap<VendorAddress, VendorAddressDto>();
            
        // DTO to Entity (only maps matching properties, ignores the rest)
        CreateMap<CreateVendorProfileDto, VendorProfile>();
        CreateMap<CreateVendorProfileDto, VendorAddress>()
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building ?? string.Empty));
        CreateMap<UpdateVendorProfileDto, VendorProfile>();
        CreateMap<UpdateVendorProfileDto, VendorAddress>()
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building ?? string.Empty));

    }
}