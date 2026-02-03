using Replate.Domain.Enums;

namespace Replate.Application.Features.Orders.DTOs;

public class OrderDto
{
    public Guid PublicId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime? PickupTime { get; set; }
    public Guid UserPublicId { get; set; }
    public string? UserDisplayName { get; set; }
    public Guid FoodListingPublicId { get; set; }
    public string FoodListingTitle { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
