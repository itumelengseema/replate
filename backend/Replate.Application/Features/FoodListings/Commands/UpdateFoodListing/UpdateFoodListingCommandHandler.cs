using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
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
        var foodListing = await _db.FoodListings
            .Include(f => f.VendorProfile)
            .Include(f => f.Items)
            .FirstOrDefaultAsync(f => f.PublicId == request.PublicId, cancellationToken);

        if (foodListing == null)
        {
            return Result<FoodListingDto>.Failure("Food listing not found");
        }

        // Update properties
        _mapper.Map(request.FoodListing, foodListing);
        foodListing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<FoodListingDto>(foodListing);
        return Result<FoodListingDto>.Success(result);
    }
}
