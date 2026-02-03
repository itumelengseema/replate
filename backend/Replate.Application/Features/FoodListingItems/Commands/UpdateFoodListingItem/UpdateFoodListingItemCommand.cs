using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;

namespace Replate.Application.Features.FoodListingItems.Commands.UpdateFoodListingItem;

public class UpdateFoodListingItemCommand : IRequest<Result<FoodListingItemDto>>
{
    public int Id { get; set; }
    public UpdateFoodListingItemDto FoodListingItem { get; set; } = null!;
}
