using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Commands.DeleteFoodListing;

public class DeleteFoodListingCommand : IRequest<Result<FoodListingDto>>
{
    public Guid PublicId { get; set; }
}
