using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;

namespace Replate.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result<OrderDto>>
{
    public int UserId { get; set; }
    public CreateOrderDto Order { get; set; } = null!;
}
