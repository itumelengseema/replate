using AutoMapper;
using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Reservations.DTOs;
using Replate.Application.Interface;

namespace Replate.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand , Result<ReservationDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateReservationCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Result<ReservationDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        // Map the DTO to the Reservation entity
      var reservationEntity = _mapper.Map<Domain.Entities.Reservation>(request.CreateReservationDto);
       
      // Set the UserId from the command
      reservationEntity.UserId = request.UserId;
      reservationEntity.CreatedAt = DateTime.UtcNow;
      reservationEntity.UpdatedAt = reservationEntity.CreatedAt;
      // Calculate TotalPrice based on Deal's DiscountedPrice and Quantity
        var deal = await _dbContext.Deals.FindAsync(new object[] { reservationEntity.DealId }, cancellationToken);
        
        if (deal == null)
        {
            return Result<ReservationDto>.Failure("Deal not found.");
        }
        
        reservationEntity.TotalPrice = deal.DiscountedPrice * reservationEntity.Quantity;
        
        
      
        // Add the Reservation entity to the database
        _dbContext.Reservations.Add(reservationEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result<ReservationDto>.Success(_mapper.Map<ReservationDto>(reservationEntity));
      
    }
}