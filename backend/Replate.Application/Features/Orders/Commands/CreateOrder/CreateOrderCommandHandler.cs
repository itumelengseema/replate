using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;

namespace Replate.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDto>>
{
    private readonly IApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            return Result<OrderDto>.Failure("User not found");
        }

        // Check if food listing exists
        var foodListing = await _db.FoodListings
            .FirstOrDefaultAsync(f => f.PublicId == request.Order.FoodListingPublicId, cancellationToken);

        if (foodListing == null)
        {
            return Result<OrderDto>.Failure("Food listing not found");
        }

        // Check availability
        if (foodListing.QuantityAvailable < request.Order.Quantity)
        {
            return Result<OrderDto>.Failure("Not enough quantity available");
        }

        // Create order
        var order = new Order
        {
            UserId = request.UserId,
            FoodListingId = foodListing.Id,
            Quantity = request.Order.Quantity,
            TotalPrice = foodListing.DiscountedPrice * request.Order.Quantity,
            Notes = request.Order.Notes,
            PickupTime = request.Order.PickupTime
        };

        // Update food listing quantity
        foodListing.QuantityAvailable -= request.Order.Quantity;

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(cancellationToken);

        // Reload with relationships for mapping
        await _db.Orders.Entry(order).Reference(o => o.User).LoadAsync(cancellationToken);
        await _db.Orders.Entry(order).Reference(o => o.FoodListing).LoadAsync(cancellationToken);

        var result = _mapper.Map<OrderDto>(order);
        return Result<OrderDto>.Success(result);
    }
}
