using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

// DEPRECATED: Use Replate.Application.Features.FoodListings.Commands.CreateFoodListingCommand instead.
namespace Replate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommand : IRequest<Result<DealDto>>
{
    public int DealId { get; set; }
   
    public CreateDealDto CreateDealDto { get; set; } = null!;
}