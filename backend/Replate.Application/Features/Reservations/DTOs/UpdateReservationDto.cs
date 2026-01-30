using Replate.Domain.Enums;

namespace Replate.Application.Features.Reservations.DTOs;

public class UpdateReservationDto
{
    public int Quantity { get; set; }
    public DateTime PickupTime { get; set; }
    public string? PickupInstructions { get; set; }
    public ReservationStatus Status { get; set; }
}