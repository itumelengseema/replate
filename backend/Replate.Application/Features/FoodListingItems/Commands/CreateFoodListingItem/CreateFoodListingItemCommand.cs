using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItems.DTOs;

namespace Replate.Application.Features.FoodListingItems.Commands.CreateFoodListingItem;

public class CreateFoodListingItemCommand : IRequest<Result<FoodListingItemDto>>
{
    public CreateFoodListingItemDto FoodListingItem { get; set; } = null!;
}
