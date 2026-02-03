using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListingItems.Queries.GetFoodListingItemsByFoodListing;

public class GetFoodListingItemsByFoodListingQueryHandler : IRequestHandler<GetFoodListingItemsByFoodListingQuery, Result<List<FoodListingItemDto>>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetFoodListingItemsByFoodListingQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<FoodListingItemDto>>> Handle(GetFoodListingItemsByFoodListingQuery request, CancellationToken cancellationToken)
    {
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(f => f.PublicId == request.FoodListingPublicId, cancellationToken);

        if (foodListing == null)
        {
            return Result<List<FoodListingItemDto>>.Failure("Food listing not found");
        }

        var foodListingItems = await _db.FoodListingItems
            .Include(f => f.FoodListing)
            .Where(f => f.FoodListingId == foodListing.Id)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<FoodListingItemDto>>(foodListingItems);
        return Result<List<FoodListingItemDto>>.Success(result);
    }
}
