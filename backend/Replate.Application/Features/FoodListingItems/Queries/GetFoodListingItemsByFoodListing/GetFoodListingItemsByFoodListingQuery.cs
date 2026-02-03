using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;

namespace Replate.Application.Features.FoodListingItems.Queries.GetFoodListingItemsByFoodListing;

public class GetFoodListingItemsByFoodListingQuery : IRequest<Result<List<FoodListingItemDto>>>
{
    public Guid FoodListingPublicId { get; set; }
}
