using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Commands.CreateDeal;

public class CreateDealCommand : IRequest<Result<DealDto>>
{
    public int DealId { get; set; }
    public CreateDealDto Deal { get; set; } = null!;
}