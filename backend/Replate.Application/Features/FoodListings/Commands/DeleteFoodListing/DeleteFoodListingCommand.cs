using MediatR;
using Replate.Application.Common.Models;

namespace Replate.Application.Features.FoodListings.Commands.DeleteFoodListing;

public class DeleteFoodListingCommand : IRequest<Result<bool>>
{
    public Guid PublicId { get; set; }
}
