using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListingItems.Queries.GetFoodListingItemById;

public class GetFoodListingItemByIdQueryHandler : IRequestHandler<GetFoodListingItemByIdQuery, Result<FoodListingItemDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetFoodListingItemByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<FoodListingItemDto>> Handle(GetFoodListingItemByIdQuery request, CancellationToken cancellationToken)
    {
        var foodListingItem = await _db.FoodListingItems
            .Include(f => f.FoodListing)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (foodListingItem == null)
        {
            return Result<FoodListingItemDto>.Failure("Food listing item not found");
        }

        var result = _mapper.Map<FoodListingItemDto>(foodListingItem);
        return Result<FoodListingItemDto>.Success(result);
    }
}
