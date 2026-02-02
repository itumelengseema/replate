using MediatR;
using Replate.Application.Common.Models;
using Replate.Application.Features.Reservations.DTOs;

namespace Replate.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommand : IRequest<Result<ReservationDto>>
{
    public int UserId { get; set; }
    public Guid FoodListingPublicId { get; set; }
}
