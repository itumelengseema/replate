using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<OrderDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _db.Orders
            .Include(o => o.User)
            .Include(o => o.FoodListing)
            .FirstOrDefaultAsync(o => o.PublicId == request.PublicId, cancellationToken);

        if (order == null)
        {
            return Result<OrderDto>.Failure("Order not found");
        }

        // Update properties
        order.Quantity = request.Order.Quantity;
        order.Status = request.Order.Status;
        order.Notes = request.Order.Notes;
        order.PickupTime = request.Order.PickupTime;
        order.TotalPrice = order.FoodListing.DiscountedPrice * request.Order.Quantity;
        order.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<OrderDto>(order);
        return Result<OrderDto>.Success(result);
    }
}
