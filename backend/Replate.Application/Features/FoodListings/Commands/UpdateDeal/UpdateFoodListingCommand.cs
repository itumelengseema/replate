using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Commands.UpdateFoodListing;

public class UpdateFoodListingCommand : IRequest<Result<FoodListingDto>>
{
    public Guid FoodListingId { get; set; }
    public UpdateFoodListingDto UpdateFoodListingDto { get; set; } = null!;
}