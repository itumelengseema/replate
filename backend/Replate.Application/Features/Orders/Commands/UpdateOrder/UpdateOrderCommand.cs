using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;

namespace Replate.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<Result<OrderDto>>
{
    public Guid PublicId { get; set; }
    public UpdateOrderDto Order { get; set; } = null!;
}
