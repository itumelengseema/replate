using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _db.Orders
            .Include(o => o.User)
            .Include(o => o.FoodListing)
            .FirstOrDefaultAsync(o => o.PublicId == request.PublicId, cancellationToken);

        if (order == null)
        {
            return Result<OrderDto>.Failure("Order not found");
        }

        var result = _mapper.Map<OrderDto>(order);
        return Result<OrderDto>.Success(result);
    }
}
