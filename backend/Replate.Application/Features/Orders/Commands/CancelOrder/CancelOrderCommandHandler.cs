using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Interface;
using Replate.Domain.Enums;

namespace Replate.Application.Features.Orders.Commands.CancelOrder;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result<bool>>
{
    private readonly IApplicationDbContext _db;

    public CancelOrderCommandHandler(IApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Result<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _db.Orders
            .Include(o => o.FoodListing)
            .FirstOrDefaultAsync(o => o.PublicId == request.PublicId, cancellationToken);

        if (order == null)
        {
            return Result<bool>.Failure("Order not found");
        }

        if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
        {
            return Result<bool>.Failure("Cannot cancel a completed or already cancelled order");
        }

        // Restore the quantity to the food listing
        order.FoodListing.QuantityAvailable += order.Quantity;
        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
