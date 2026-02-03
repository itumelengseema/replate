using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<List<OrderDto>>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetAllOrdersQueryHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<List<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _db.Orders
            .Include(o => o.User)
            .Include(o => o.FoodListing)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);

        var result = _mapper.Map<List<OrderDto>>(orders);
        return Result<List<OrderDto>>.Success(result);
    }
}
