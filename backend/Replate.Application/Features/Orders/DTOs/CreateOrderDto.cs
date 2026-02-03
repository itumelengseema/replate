namespace Replate.Application.Features.Orders.DTOs;

public class CreateOrderDto
{
    public Guid FoodListingPublicId { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
    public DateTime? PickupTime { get; set; }
}
