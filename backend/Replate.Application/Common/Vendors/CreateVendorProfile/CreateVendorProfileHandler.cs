using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Interfaces;
using Replate.Application.DTOs;
using Replate.Domain.Entities;



namespace Replate.Application.Common.Vendors.CreateVendorProfile;

public class CreateVendorProfileHandler(IApplicationDbContext db, IMapper mapper)
{
    private readonly IApplicationDbContext _db = db;
    private readonly IMapper _mapper = mapper;

    public async Task<VendorProfileDto> HandleAsync(CreateVendorProfileCommand request,
        CancellationToken cancellationToken = default)
    {
        // Validate user exists
        var userExists = await _db.Users
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
        {
            throw new InvalidCastException($"User with ID  {request.UserId} not found ");
            
        }
        
        // Checks if user already has a vendor profile
        
        var existingProfile = await _db.VendorProfiles
            .AnyAsync(v => v.UserId == request.UserId,cancellationToken);
        if (existingProfile)
        {
            throw new InvalidOperationException("User already has a vendor profile");
        }
        
        // create vendor profile
        //TODO try use auto mapper to map out properties automatically
        var vendorProfile = new VendorProfile
        {
            UserId = request.UserId,
            BusinessName = request.BusinessName,
            Decscription = request.Description,
            PhoneNumber = request.PhoneNumber,
            LogoImageUrl = request.LogoUrl,
            BannerImageUrl = request.BannerImageUrl,
            Address = new VendorAddress
            {
                Street = request.Address.Street,
                Building = request.Address.Building,
                City = request.Address.City,
                Province = request.Address.Province,
                PostalCode = request.Address.PostalCode,
                Country = request.Address.Country,
                Latitude = request.Address.Latitude,
                Longitude = request.Address.Longitude,
                GooglePlaceId = request.Address.GooglePlaceId

            }
        };

        _db.VendorProfiles.Add(vendorProfile);
        await _db.SaveChangesAsync(cancellationToken);
        return _mapper.Map<VendorProfileDto>(vendorProfile);
    }
}