using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.VendorProfiles.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.VendorProfiles.Commands.CreateVendorProfile;

public class CreateVendorProfileCommandHandler : IRequestHandler<CreateVendorProfileCommand, Result<VendorProfileDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public CreateVendorProfileCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<Result<VendorProfileDto>> Handle(
        CreateVendorProfileCommand request, CancellationToken cancellationToken)
    {
        // checks if the user exists
        var userExists =  await _db.Users
            .AnyAsync(u=> u.Id == request.UserId, cancellationToken);

        if (!userExists)
        {
            return Result<VendorProfileDto>.Failure("User not found");
        }
        
        // checks if user already has a vendor profile
        var existingProfile = await _db.VendorProfiles
            .AnyAsync(vp => vp.UserId == request.UserId, cancellationToken);

        if (existingProfile)
        {
            return Result<VendorProfileDto>.Failure("User already has a vendor profile");
        }
        
        // create vendor address
        var vendorAddress = _mapper.Map<VendorAddress>(request.VendorProfile);
        
        // create vendor profile
        var vendorProfile = _mapper.Map<VendorProfile>(request.VendorProfile);
        vendorProfile.UserId = request.UserId;
        vendorProfile.VendorAddress = vendorAddress;
        
        // add to database
        _db.VendorProfiles.Add(vendorProfile);
        await  _db.SaveChangesAsync(cancellationToken);
        
        // map to dto
        var result = _mapper.Map<VendorProfileDto>(vendorProfile);
        
        return Result<VendorProfileDto>.Success(result);
    }
}