using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.FoodListings.DTOs;

namespace Replate.Application.Features.FoodListings.Queries.GetFoodListingByIdQuery;

public class GetFoodListingById: IRequest<Result<FoodListingDto>>
{
    public Guid Id { get; set; }
}