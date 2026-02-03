using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;

namespace Replate.Application.Features.FoodListingItems.Queries.GetFoodListingItemById;

public class GetFoodListingItemByIdQuery : IRequest<Result<FoodListingItemDto>>
{
    public int Id { get; set; }
}
