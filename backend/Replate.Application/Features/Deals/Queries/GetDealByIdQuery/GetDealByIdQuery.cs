using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Deals.DTOs;

namespace Replate.Application.Features.Deals.Queries.GetDealByIdQuery;

public class GetDealByIdQuery: IRequest<Result<DealDto>>
{
    public Guid Id { get; set; }
}