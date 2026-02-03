using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListings.Commands.DeleteFoodListing;

public class DeleteFoodListingCommandHandler : IRequestHandler<DeleteFoodListingCommand, Result<bool>>
{
    private readonly IApplicationDbContext _db;

    public DeleteFoodListingCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(DeleteFoodListingCommand request, CancellationToken cancellationToken)
    {
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(f => f.PublicId == request.PublicId, cancellationToken);

        if (foodListing == null)
        {
            return Result<bool>.Failure("Food listing not found");
        }

        _db.FoodListings.Remove(foodListing);
        await _db.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
