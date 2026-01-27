using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.VendorProfiles.Commands.UpdateVendorProfile;

public class UpdateVendorProfileCommandHandler
    :IRequestHandler<UpdateVendorProfileCommand,Result<VendorProfileDto>>
{
    public readonly IApplicationDbContext _db;
    public readonly IMapper _mapper;
    
    public UpdateVendorProfileCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<VendorProfileDto>> Handle(UpdateVendorProfileCommand request, CancellationToken cancellationToken)
    {
    // find vendor profile by public id
        var vendorProfile = await _db.VendorProfiles
            .FirstOrDefaultAsync(vp => vp.PublicId == request.PublicId, cancellationToken );
        
        if (vendorProfile == null)
        {
            return  Result<VendorProfileDto>.Failure("VendorProfile not found");
        }
        
        // update vendor profile properties (onlu non-null values)
        
        if (!string.IsNullOrWhiteSpace(request.VendorProfile.BusinessName))
            vendorProfile.BusinessName = request.VendorProfile.BusinessName;

        if (request.VendorProfile.Description != null)
            vendorProfile.Description = request.VendorProfile.Description;

        if (request.VendorProfile.LogoUrl != null)
            vendorProfile.LogoUrl = request.VendorProfile.LogoUrl;

        if (!string.IsNullOrWhiteSpace(request.VendorProfile.PhoneNumber))
            vendorProfile.PhoneNumber = request.VendorProfile.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(request.VendorProfile.Email))
            vendorProfile.Email = request.VendorProfile.Email;

        if (request.VendorProfile.BusinessHours != null)
            vendorProfile.BusinessHours = request.VendorProfile.BusinessHours;
        
        
        // Update address if provided
        
        if (vendorProfile.VendorAddress != null && HasAddressUpdate(request.VendorProfile))
        {
            if (!string.IsNullOrWhiteSpace(request.VendorProfile.Street))
                vendorProfile.VendorAddress.Street = request.VendorProfile.Street;

            if (request.VendorProfile.Building != null)
                vendorProfile.VendorAddress.Building = request.VendorProfile.Building;

            if (!string.IsNullOrWhiteSpace(request.VendorProfile.City))
                vendorProfile.VendorAddress.City = request.VendorProfile.City;

            if (!string.IsNullOrWhiteSpace(request.VendorProfile.Province))
                vendorProfile.VendorAddress.Province = request.VendorProfile.Province;

            if (!string.IsNullOrWhiteSpace(request.VendorProfile.PostalCode))
                vendorProfile.VendorAddress.PostalCode = request.VendorProfile.PostalCode;

            if (!string.IsNullOrWhiteSpace(request.VendorProfile.Country))
                vendorProfile.VendorAddress.Country = request.VendorProfile.Country;

            if (request.VendorProfile.Latitude.HasValue)
                vendorProfile.VendorAddress.Latitude = request.VendorProfile.Latitude;

            if (request.VendorProfile.Longitude.HasValue)
                vendorProfile.VendorAddress.Longitude = request.VendorProfile.Longitude;
        }
        
        vendorProfile.UpdatedAt = DateTime.UtcNow;
        
        await _db.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<VendorProfileDto>(vendorProfile);
        return Result<VendorProfileDto>.Success(result);
        
        
    }

    private bool HasAddressUpdate(UpdateVendorProfileDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Street) ||
               !string.IsNullOrWhiteSpace(dto.City) ||
               !string.IsNullOrWhiteSpace(dto.Province) ||
               !string.IsNullOrWhiteSpace(dto.PostalCode) ||
               !string.IsNullOrWhiteSpace(dto.Country);
    }
}