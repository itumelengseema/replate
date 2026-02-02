using Replate.Domain.Entities;
using Replate.Domain.Enums;

namespace Replate.Application.Features.Reservations.DTOs;

public class CreateReservationDto
{

  
        
        
    // Reservation Details
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public ReservationStatus Status { get; set; }
        
    //Pickup Details
    public DateTime PickupTime { get; set; }
    public string? PickupInstructions { get; set; }

    // Which deal is reserved
    public int DealId { get; set; } 

    // Which user reserved it
    public int UserId { get; set; } 


}