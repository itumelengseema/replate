using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Commands.DeleteDeal;

public class DeleteDealCommand :IRequest<Result<DealDto>>
{
    public Guid PublicId { get; set; }
    
    
}