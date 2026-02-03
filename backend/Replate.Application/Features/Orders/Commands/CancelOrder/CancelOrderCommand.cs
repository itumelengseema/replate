using MediatR;
using Replate.Application.Common.Models;

namespace Replate.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommand : IRequest<Result<bool>>
{
    public Guid PublicId { get; set; }
}
