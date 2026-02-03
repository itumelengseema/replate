using MediatR;
using Replate.Application.Common.Models;

namespace Replate.Application.Features.FoodListingItems.Commands.DeleteFoodListingItem;

public class DeleteFoodListingItemCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
}
