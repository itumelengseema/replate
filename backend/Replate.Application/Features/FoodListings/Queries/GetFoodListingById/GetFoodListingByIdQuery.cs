using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Queries.GetFoodListingById;

public class GetFoodListingByIdQuery : IRequest<Result<FoodListingDto>>
{
    public Guid PublicId { get; set; }
}
