using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Queries.GetAllFoodListings;

public class GetAllFoodListingsQuery : IRequest<Result<List<FoodListingDto>>>
{
}
