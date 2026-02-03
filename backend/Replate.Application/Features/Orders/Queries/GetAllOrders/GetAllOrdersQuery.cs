using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;

namespace Replate.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<Result<List<OrderDto>>>
{
}
