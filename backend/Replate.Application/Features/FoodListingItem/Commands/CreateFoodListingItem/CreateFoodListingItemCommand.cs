using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListingItem.Dtos;

namespace Replate.Application.Features.FoodListingItem.Commands.CreateFoodListingItem;

public class CreateFoodListingItemCommand : IRequest<Result<FoodListingItemDto>>
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public int FoodListingId { get; set; }
}
