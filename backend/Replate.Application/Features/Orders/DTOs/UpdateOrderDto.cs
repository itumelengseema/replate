using Replate.Domain.Enums;

namespace Replate.Application.Features.Orders.DTOs;

public class UpdateOrderDto
{
    public int Quantity { get; set; }
    public DateTime PickupTime { get; set; }
    public string? PickupInstructions { get; set; }
    public OrderStatus Status { get; set; }
}
