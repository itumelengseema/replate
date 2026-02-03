using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListingItems.Commands.UpdateFoodListingItem;

public class UpdateFoodListingItemCommandHandler : IRequestHandler<UpdateFoodListingItemCommand, Result<FoodListingItemDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateFoodListingItemCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<FoodListingItemDto>> Handle(UpdateFoodListingItemCommand request, CancellationToken cancellationToken)
    {
        var foodListingItem = await _db.FoodListingItems
            .Include(f => f.FoodListing)
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (foodListingItem == null)
        {
            return Result<FoodListingItemDto>.Failure("Food listing item not found");
        }

        // Update properties
        foodListingItem.Name = request.FoodListingItem.Name;
        foodListingItem.Quantity = request.FoodListingItem.Quantity;
        foodListingItem.Description = request.FoodListingItem.Description;

        await _db.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<FoodListingItemDto>(foodListingItem);
        return Result<FoodListingItemDto>.Success(result);
    }
}
