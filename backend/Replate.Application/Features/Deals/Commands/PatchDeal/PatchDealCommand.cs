using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Commands.PatchDeal;

public class PatchDealCommand : IRequest<Result<DealDto>> // Removed duplicate IRequest interface
{
    public Guid DealId { get; set; }
    public PatchDealDto PatchDealDto { get; set; } = null!;
}