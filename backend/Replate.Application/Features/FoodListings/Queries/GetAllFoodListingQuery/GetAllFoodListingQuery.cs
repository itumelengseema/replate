using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Queries.GetAllFoodListingQuery;

public class GetAllFoodListingQuery : IRequest<Result<List<FoodListingDto>>>
{
}
