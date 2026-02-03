using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;

namespace Replate.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery : IRequest<Result<OrderDto>>
{
    public Guid PublicId { get; set; }
}
