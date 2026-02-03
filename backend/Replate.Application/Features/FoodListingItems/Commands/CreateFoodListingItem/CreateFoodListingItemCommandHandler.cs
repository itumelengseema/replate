using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.FoodListingItems.Commands.CreateFoodListingItem;

public class CreateFoodListingItemCommandHandler : IRequestHandler<CreateFoodListingItemCommand, Result<FoodListingItemDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateFoodListingItemCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<FoodListingItemDto>> Handle(CreateFoodListingItemCommand request, CancellationToken cancellationToken)
    {
        // Find the food listing by PublicId
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(f => f.PublicId == request.FoodListingItem.FoodListingPublicId, cancellationToken);

        if (foodListing == null)
        {
            return Result<FoodListingItemDto>.Failure("Food listing not found");
        }

        // Create the food listing item
        var foodListingItem = _mapper.Map<FoodListingItem>(request.FoodListingItem);
        foodListingItem.FoodListingId = foodListing.Id;

        _db.FoodListingItems.Add(foodListingItem);
        await _db.SaveChangesAsync(cancellationToken);

        // Reload with food listing for mapping
        await _db.FoodListingItems
            .Entry(foodListingItem)
            .Reference(f => f.FoodListing)
            .LoadAsync(cancellationToken);

        var result = _mapper.Map<FoodListingItemDto>(foodListingItem);
        return Result<FoodListingItemDto>.Success(result);
    }
}
