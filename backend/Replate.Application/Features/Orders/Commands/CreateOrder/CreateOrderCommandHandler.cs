using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Replate.Application.Common.Models;
using Replate.Application.Features.Orders.DTOs;
using Replate.Application.Features.Reservations.DTOs;
using Replate.Application.Interface;
using Replate.Domain.Entities;
using Replate.Domain.Enums;

namespace Replate.Application.Features.Reservations.Commands.CreateReservation;

public class CreateOrderCommandHandler : IRequestHandler<CreateReservationCommand , Result<OrderDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Result<OrderDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the food listing to get its DiscountedPrice
        var foodListing = await _dbContext.FoodListings
            .FirstOrDefaultAsync(d => d.PublicId == request.FoodListingPublicId, cancellationToken);

        if (foodListing == null)
        {
            return Result<OrderDto>.Failure("Food listing not found.");
        }
        
        // Check if a reservation already exists for the same user and food listing
        var reservationExists = await _dbContext.Orders
            .AnyAsync(r => r.UserId == request.UserId && r.FoodListingId == foodListing.Id, cancellationToken);

        if (reservationExists)
        {
            return Result<OrderDto>.Failure("Reservation already exists for this user and food listing.");
        }
        
        // Map the command to a Reservation entity
        var reservationEntity = new Order()
        {
            FoodListingId = foodListing.Id,
            Quantity = 1, // Default to 1
            Status = OrderStatus.Pending // Default status
        };

        // Set the UserId from the command
        reservationEntity.UserId = request.UserId;
        reservationEntity.CreatedAt = DateTime.UtcNow;
        reservationEntity.UpdatedAt = reservationEntity.CreatedAt;

        // Calculate TotalPrice based on FoodListing's DiscountedPrice and Quantity
        reservationEntity.TotalPrice = foodListing.DiscountedPrice * reservationEntity.Quantity;

        // Add the Reservation entity to the database
        _dbContext.Orders.Add(reservationEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result<OrderDto>.Success(_mapper.Map<OrderDto>(reservationEntity));

    }
}