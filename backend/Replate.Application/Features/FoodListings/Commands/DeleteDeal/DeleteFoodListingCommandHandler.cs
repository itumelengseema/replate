using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.Commands.DeleteFoodListing;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListings.Commands.DeleteDeal;

public class DeleteFoodListingCommandHandler : IRequestHandler<DeleteFoodListingCommand, Result<FoodListingDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public DeleteFoodListingCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<Result<FoodListingDto>> Handle(DeleteFoodListingCommand request, CancellationToken cancellationToken)
    {
        // Check if Food Listing Exists
        var foodListingExists = _db.FoodListings
            .AnyAsync(d => d.PublicId == request.PublicId, cancellationToken);

        if (!await foodListingExists)
        {
            return Result<FoodListingDto>.Failure("Food listing not found");
        }
        
        // Delete Food Listing
        var foodListingToDelete = await _db.FoodListings
            .FirstOrDefaultAsync(d => d.PublicId == request.PublicId, cancellationToken);
        
        _db.FoodListings.Remove(foodListingToDelete!);
        await _db.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<FoodListingDto>(foodListingToDelete);
        return Result<FoodListingDto>.Success(result);
    }
}
