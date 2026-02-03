using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListingItems.Commands.DeleteFoodListingItem;

public class DeleteFoodListingItemCommandHandler : IRequestHandler<DeleteFoodListingItemCommand, Result<bool>>
{
    private readonly IApplicationDbContext _db;

    public DeleteFoodListingItemCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(DeleteFoodListingItemCommand request, CancellationToken cancellationToken)
    {
        var foodListingItem = await _db.FoodListingItems
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (foodListingItem == null)
        {
            return Result<bool>.Failure("Food listing item not found");
        }

        _db.FoodListingItems.Remove(foodListingItem);
        await _db.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
