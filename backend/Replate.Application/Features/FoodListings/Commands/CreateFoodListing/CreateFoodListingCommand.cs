using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Commands.CreateFoodListing;

public class CreateFoodListingCommand : IRequest<Result<FoodListingDto>>
{
    public int VendorProfileId { get; set; }
    public CreateFoodListingDto FoodListing { get; set; } = null!;
}
