using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.Commands.UpdateDeal;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListings.Commands.UpdateFoodListing;

public class UpdateFoodListingCommandHandler : IRequestHandler<UpdateFoodListingCommand, Result<FoodListingDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateFoodListingCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<FoodListingDto>> Handle(UpdateFoodListingCommand request, CancellationToken cancellationToken)
    {
        // Find the food listing by public ID
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(d => d.PublicId == request.FoodListingId, cancellationToken);

        if (foodListing == null)
        {
            return Result<FoodListingDto>.Failure("Food listing not found");
        }
        
        //Update the food listing properties
        if (!string.IsNullOrWhiteSpace(request.UpdateFoodListingDto.Title))
            foodListing.Title = request.UpdateFoodListingDto.Title;
        if (!string.IsNullOrWhiteSpace(request.UpdateFoodListingDto.Description))
            foodListing.Description = request.UpdateFoodListingDto.Description;
        if (request.UpdateFoodListingDto.OriginalPrice.HasValue)
            foodListing.OriginalPrice = request.UpdateFoodListingDto.OriginalPrice.Value;
        if (request.UpdateFoodListingDto.DiscountedPrice.HasValue)
            foodListing.DiscountedPrice = request.UpdateFoodListingDto.DiscountedPrice.Value;
        if (request.UpdateFoodListingDto.AvailableQuantity.HasValue)
            foodListing.AvailableQuantity = request.UpdateFoodListingDto.AvailableQuantity.Value;
        if (request.UpdateFoodListingDto.FoodListingType.HasValue)
            foodListing.FoodListingType = request.UpdateFoodListingDto.FoodListingType.Value;
        if (request.UpdateFoodListingDto.Category.HasValue)
            foodListing.Category = request.UpdateFoodListingDto.Category.Value;
        if (request.UpdateFoodListingDto.AvailableFrom.HasValue)
            foodListing.AvailableFrom = request.UpdateFoodListingDto.AvailableFrom.Value;
        if (request.UpdateFoodListingDto.AvailableUntil.HasValue)
            foodListing.AvailableUntil = request.UpdateFoodListingDto.AvailableUntil.Value;
        if (request.UpdateFoodListingDto.VendorProfileId.HasValue)
            foodListing.VendorProfileId = request.UpdateFoodListingDto.VendorProfileId.Value;
       
        //Update the UpdatedAt timestamp
        foodListing.UpdatedAt = DateTime.UtcNow;
        
        // Save changes to the database
        await _db.SaveChangesAsync(cancellationToken);
        
        // Map to DTO and return success result
        var foodListingDto = _mapper.Map<FoodListingDto>(foodListing);
        return Result<FoodListingDto>.Success(foodListingDto);
    }
}
