using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.FoodListings.Commands.CreateFoodListing;

public class CreateFoodListingCommandHandler : IRequestHandler<CreateFoodListingCommand, Result<FoodListingDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateFoodListingCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<FoodListingDto>> Handle(CreateFoodListingCommand request, CancellationToken cancellationToken)
    {
        // Check if vendor profile exists
        var vendorProfile = await _db.VendorProfiles
            .FirstOrDefaultAsync(v => v.Id == request.VendorProfileId, cancellationToken);

        if (vendorProfile == null)
        {
            return Result<FoodListingDto>.Failure("Vendor profile not found");
        }

        // Create food listing
        var foodListing = _mapper.Map<FoodListing>(request.FoodListing);
        foodListing.VendorProfileId = request.VendorProfileId;

        // Map items
        foreach (var itemDto in request.FoodListing.Items)
        {
            var item = _mapper.Map<FoodListingItem>(itemDto);
            foodListing.Items.Add(item);
        }

        _db.FoodListings.Add(foodListing);
        await _db.SaveChangesAsync(cancellationToken);

        // Reload with vendor profile for mapping
        await _db.FoodListings
            .Entry(foodListing)
            .Reference(f => f.VendorProfile)
            .LoadAsync(cancellationToken);

        var result = _mapper.Map<FoodListingDto>(foodListing);
        return Result<FoodListingDto>.Success(result);
    }
}
