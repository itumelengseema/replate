using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;

namespace Replate.Application.Features.Orders.Queries.GetOrdersByUser;

public class GetOrdersByUserQuery : IRequest<Result<List<OrderDto>>>
{
    public int UserId { get; set; }
}
