using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.FoodListings.Queries.GetAllFoodListingQuery;

public class GetAllFoodListingQueryHandler : IRequestHandler<GetAllFoodListingQuery, Result<List<FoodListingDto>>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetAllFoodListingQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<Result<List<FoodListingDto>>> Handle(GetAllFoodListingQuery request, CancellationToken cancellationToken)
    {
        // get all food listings from database
        var foodListings = await _db.FoodListings.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<FoodListingDto>>(foodListings);
        return Result<List<FoodListingDto>>.Success(result);
    }
}
