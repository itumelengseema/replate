using Replate.Domain.Enums;

namespace Replate.Application.Features.Orders.DTOs;

public class UpdateOrderDto
{
    public int Quantity { get; set; }
    public OrderStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime? PickupTime { get; set; }
}
