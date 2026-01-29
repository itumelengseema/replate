using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Commands.UpdateDeal;

public class UpdateDealCommand : IRequest<Result<DealDto>>
{
    public Guid DealId { get; set; }
    public UpdateDealDto UpdateDealDto { get; set; } = null!;
}