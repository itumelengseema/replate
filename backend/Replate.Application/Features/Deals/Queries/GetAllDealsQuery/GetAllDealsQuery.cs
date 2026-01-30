using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Queries.GetAllDealsQuery;

public class GetAllDealsQuery : IRequest<Result<List<DealDto>>>
{
}