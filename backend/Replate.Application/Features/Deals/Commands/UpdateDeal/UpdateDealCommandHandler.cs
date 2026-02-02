using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Interface;


namespace Replate.Application.Features.Deals.Commands.UpdateDeal;



public class UpdateDealCommandHandler :
    IRequestHandler<UpdateDealCommand, Result<DealDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateDealCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<DealDto>> Handle(UpdateDealCommand request, CancellationToken cancellationToken)
    {
        // Find the food listing by public ID
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(d => d.PublicId == request.DealId, cancellationToken);

        if (foodListing == null)
        {
            return Result<DealDto>.Failure("Deal not found");
        }
        
        //Update the food listing properties
        if (!string.IsNullOrWhiteSpace(request.UpdateDealDto.Title))
            foodListing.Title = request.UpdateDealDto.Title;
        if (!string.IsNullOrWhiteSpace(request.UpdateDealDto.Description))
            foodListing.Description = request.UpdateDealDto.Description;
        if (request.UpdateDealDto.OriginalPrice.HasValue)
            foodListing.OriginalPrice = request.UpdateDealDto.OriginalPrice.Value;
        if (request.UpdateDealDto.DiscountedPrice.HasValue)
            foodListing.DiscountedPrice = request.UpdateDealDto.DiscountedPrice.Value;
        if (request.UpdateDealDto.AvailableQuantity.HasValue)
            foodListing.AvailableQuantity = request.UpdateDealDto.AvailableQuantity.Value;
        if (request.UpdateDealDto.FoodListingType.HasValue)
            foodListing.FoodListingType = request.UpdateDealDto.FoodListingType.Value;
        if (request.UpdateDealDto.Category.HasValue)
            foodListing.Category = request.UpdateDealDto.Category.Value;
        if (request.UpdateDealDto.AvailableFrom.HasValue)
            foodListing.AvailableFrom = request.UpdateDealDto.AvailableFrom.Value;
        if (request.UpdateDealDto.AvailableUntil.HasValue)
            foodListing.AvailableUntil = request.UpdateDealDto.AvailableUntil.Value;
        if (request.UpdateDealDto.VendorProfileId.HasValue)
            foodListing.VendorProfileId = request.UpdateDealDto.VendorProfileId.Value;
       
        //Update the UpdatedAt timestamp
        foodListing.UpdatedAt = DateTime.UtcNow;
        
        // Save changes to the database
        await _db.SaveChangesAsync(cancellationToken);
        // Map to DTO and return success result
        var dealDto = _mapper.Map<DealDto>(foodListing);
        return Result<DealDto>.Success(dealDto);
    }


}
