using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListings.Queries.GetFoodListingByIdQuery;

public class GetFoodListingByIdQueryHandler : IRequestHandler<GetFoodListingById, Result<FoodListingDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetFoodListingByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<Result<FoodListingDto>> Handle(GetFoodListingById request, CancellationToken cancellationToken)
    {
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(d => d.PublicId == request.Id, cancellationToken);
        
        if (foodListing == null)
        {
            return Result<FoodListingDto>.Failure("Food listing not found");
        }
        
        var result = _mapper.Map<FoodListingDto>(foodListing);
        return Result<FoodListingDto>.Success(result);
    }
}
