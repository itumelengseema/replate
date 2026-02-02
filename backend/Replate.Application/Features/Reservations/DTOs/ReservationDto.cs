using Replate.Application.Features.Deals.DTOs;
using Replate.Application.Features.Users.DTOs;

using Replate.Domain.Enums;

namespace Replate.Application.Features.Reservations.DTOs;

public class ReservationDto
{

    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    // Reservation Details
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public ReservationStatus Status { get; set; }
        
    //Pickup Details
    public DateTime PickupTime { get; set; }
    public string? PickupInstructions { get; set; }

    // Which deal is reserved
    public int DealId { get; set; }
    public DealDto Deal { get; set; } 

    // Which user reserved it
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}