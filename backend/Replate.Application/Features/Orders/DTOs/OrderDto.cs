using Replate.Application.Features.FoodListings.DTOs;
using Replate.Application.Features.Users.DTOs;
using Replate.Domain.Enums;

namespace Replate.Application.Features.Orders.DTOs;

public class OrderDto
{
    public Guid PublicId { get; set; } = Guid.NewGuid();
    
    // Order Details
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
        
    //Pickup Details
    public DateTime PickupTime { get; set; }
    public string? PickupInstructions { get; set; }

    // Which food listing is ordered
    public int FoodListingId { get; set; }
    public FoodListingDto FoodListing { get; set; } = null!;

    // Which user ordered it
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;

    // Audit fields
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}