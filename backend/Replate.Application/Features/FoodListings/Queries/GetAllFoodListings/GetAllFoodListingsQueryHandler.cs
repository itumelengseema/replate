using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListings.Queries.GetAllFoodListings;

public class GetAllFoodListingsQueryHandler : IRequestHandler<GetAllFoodListingsQuery, Result<List<FoodListingDto>>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetAllFoodListingsQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<FoodListingDto>>> Handle(GetAllFoodListingsQuery request, CancellationToken cancellationToken)
    {
        var foodListings = await _db.FoodListings
            .Include(f => f.VendorProfile)
            .Include(f => f.Items)
            .Where(f => f.IsActive)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<FoodListingDto>>(foodListings);
        return Result<List<FoodListingDto>>.Success(result);
    }
}
