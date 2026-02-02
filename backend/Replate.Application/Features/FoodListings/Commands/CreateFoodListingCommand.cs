using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Commands.CreateFoodListing;

public class CreateFoodListingCommand : IRequest<Result<FoodListingDto>>
{
    public int FoodListingId { get; set; }
    public CreateFoodListingDto CreateFoodListingDto { get; set; } = null!;
}
